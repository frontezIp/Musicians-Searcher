using Chat.BusinessLogic.DTOs.RequestDTOs.ChatParticipantRequestsDTOs;
using Chat.BusinessLogic.DTOs.ResponseDTOs;
using Chat.BusinessLogic.Exceptions.AlreadyExistsException;
using Chat.BusinessLogic.Exceptions.BadRequestException;
using Chat.BusinessLogic.Extensions;
using Chat.BusinessLogic.Helpers.Interfaces;
using Chat.BusinessLogic.RequestFeatures;
using Chat.BusinessLogic.Services.Interfaces;
using Chat.DataAccess.Models;
using Chat.DataAccess.Options;
using Chat.DataAccess.Repositories.Interfaces;
using Chat.DataAccess.RequestParameters;
using Chat.DataAccess.Specifications.ChatParticipantSpecifications;
using MapsterMapper;
using Microsoft.Extensions.Options;

namespace Chat.BusinessLogic.Services.Implementations
{
    internal class ChatParticipantService : IChatParticipantService
    {
        private readonly IChatParticipantServiceHelper _chatParticipantServiceHelper;
        private readonly IChatParticipantRepository _chatParticipantRepository;
        private readonly IChatRoomServiceHelper _chatRoomServiceHelper;
        private readonly IMessengerUserServiceHelper _messengerUserServiceHelper;
        private readonly IChatRoleServiceHelper _chatRoleServiceHelper;
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IOptions<ChatRolesOptions> _chatRolesOptions;
        private readonly IMapper _mapper;

        public ChatParticipantService(IChatParticipantServiceHelper chatParticipantServiceHelper,
            IChatParticipantRepository chatParticipantRepository,
            IMapper mapper,
            IChatRoomServiceHelper chatRoomServiceHelper,
            IMessengerUserServiceHelper messengerUserServiceHelper,
            IOptions<ChatRolesOptions> options,
            IChatRoleServiceHelper chatRoleServiceHelper,
            IChatRoomRepository chatRoomRepository)
        {
            _chatParticipantServiceHelper = chatParticipantServiceHelper;
            _chatParticipantRepository = chatParticipantRepository;
            _mapper = mapper;
            _chatRoomServiceHelper = chatRoomServiceHelper;
            _messengerUserServiceHelper = messengerUserServiceHelper;
            _chatRolesOptions = options;
            _chatRoleServiceHelper = chatRoleServiceHelper;
            _chatRoomRepository = chatRoomRepository;
        }

        public async Task<(IEnumerable<ChatParticipantResponseDto> chatParticipants, MetaData metaData)> GetChatParticipantsAsync(Guid messengerUserId,
            Guid chatRoomId,
            GetFilteredChatParticipantsRequestDto getFilteredChatParticipantsRequestDTO,
            CancellationToken cancellationToken = default)
        {
            await _messengerUserServiceHelper.CheckIfMessengerUserExistsAsync(messengerUserId, cancellationToken);
            await _chatRoomServiceHelper.CheckIfChatRoomExistsAsync(chatRoomId, cancellationToken);
            await _chatParticipantServiceHelper.CheckIfChatParticipantExistsByGivenMessengerUserAndChatRoomIdAsync(messengerUserId, chatRoomId, cancellationToken);

            var chatParticipantsParameters = _mapper.Map<ChatParticipantRequestParameters>(getFilteredChatParticipantsRequestDTO);
            var spec = new GetChatParticipantsOfTheGivenChatRoomWithPagingSpecification(chatRoomId, chatParticipantsParameters);

            var chatParticipants = await _chatParticipantRepository.GetBySpecificationAsync(spec, cancellationToken);
            chatParticipants = chatParticipants.ToList();
            var count = await _chatParticipantRepository.CountBySpecificationAsync(spec, cancellationToken);

            var metadata = chatParticipants.GetMetaData(count, getFilteredChatParticipantsRequestDTO.PageNumber, getFilteredChatParticipantsRequestDTO.PageSize);

            var chatParticipantsDtos = _mapper.Map<IEnumerable<ChatParticipantResponseDto>>(chatParticipants);

            return (chatParticipants: chatParticipantsDtos, metaData: metadata);
         }

        public async Task AddChatParticipantsAsync(Guid messengerUserId,
            Guid chatRoomId,
            List<Guid> messengerUserIdsToAdd, 
            CancellationToken cancellationToken = default)
        {
            var chatRoom = await _chatRoomServiceHelper.CheckIfChatRoomExistsAndGetByIdAsync(chatRoomId, true, cancellationToken);
            await _chatParticipantServiceHelper.CheckIfChatParticipantExistsByGivenMessengerUserAndChatRoomIdAsync(messengerUserId, chatRoomId, cancellationToken);
            var messengerUsers = await _messengerUserServiceHelper.CheckIfMessengerUsersByGivenIdsExistsAndGetAsync(messengerUserIdsToAdd, false, cancellationToken);

            if (await _chatParticipantRepository.IsAnyOfTheGivenUsersAlreadyChatParticipant(chatRoomId, messengerUserIdsToAdd, cancellationToken))
                throw new MessengerUsersAlreadyExistsAsChatParticipantsException($"Some of the given messenger users" +
                    $" are already exists as chat participants in the chat room with {chatRoomId} id");

            var chatRole = await _chatRoleServiceHelper.CheckIfChatRoleExistsAndGetByNameAsync(_chatRolesOptions.Value.User, false, cancellationToken);

            var possibleChatParticipants = messengerUsers.Select(user => new ChatParticipant
            {
                ChatRoleId = chatRole.Id,
                MessengerUserId = user.Id,
                ChatRoomId = chatRoomId
            });

            await _chatParticipantRepository.AddManyAsync(possibleChatParticipants);
            chatRoom.MembersNumber += possibleChatParticipants.Count();
            _chatRoomRepository.Update(chatRoom);
            await _chatRoomRepository.SaveChangesAsync();
        }

        public async Task DeleteChatParticipantsAsync(Guid messengerUserId,
            Guid chatRoomId,
            List<Guid> chatParticipantsToDelete, 
            CancellationToken cancellationToken)
        {
            var chatRoom = await _chatRoomServiceHelper.CheckIfChatRoomExistsAndGetByIdAsync(chatRoomId, true, cancellationToken);
            var chatParticipant = await _chatParticipantServiceHelper.CheckIfChatParticipantExistsByGivenMessengerUserAndChatRoomIdAndGetAsync(messengerUserId, chatRoomId, false, cancellationToken);
            await _chatParticipantServiceHelper.CheckIfChatParticipantsExistsAsync(chatParticipantsToDelete, cancellationToken);

            var chatParticipantsSpec = new GetChatParticipantsWithIncludedChatRoleSpecification(chatParticipantsToDelete, true);

            var chatParticipants = await _chatParticipantRepository.GetBySpecificationAsync(chatParticipantsSpec, cancellationToken);

            if (!chatParticipants.All(participant => _chatRoleServiceHelper.IsFirstRoleHasMorePrecedenceThenSecond(chatParticipant.ChatRole, participant.ChatRole)))
                throw new DeleteChatParticipantWithHigherOrEqualChatRoleBadRequestException(chatParticipant.Id);

            _chatParticipantRepository.DeleteMany(chatParticipants);
            chatRoom.MembersNumber -= chatParticipants.Count();
            _chatRoomRepository.Update(chatRoom);
            await _chatParticipantRepository.SaveChangesAsync();
        }

        public async Task UpdateChatParticipantRoleAsync(Guid messengerUserIssuerId, Guid chatRoomId, Guid chatParticipantToUpdateId, Guid newRoleId, CancellationToken cancellationToken = default)
        {
            await _messengerUserServiceHelper.CheckIfMessengerUserExistsAsync(messengerUserIssuerId, cancellationToken);
            await _chatRoomServiceHelper.CheckIfChatRoomExistsAsync(chatRoomId, cancellationToken);
            var issuer = await _chatParticipantServiceHelper.CheckIfChatParticipantExistsByGivenMessengerUserAndChatRoomIdAndGetAsync(messengerUserIssuerId, chatRoomId, false, cancellationToken);
            var chatParticipantToUpdate = await _chatParticipantServiceHelper.CheckIfChatParticipantExistsAndGetAsync(chatRoomId, chatParticipantToUpdateId, true, cancellationToken);
            var newRole = await _chatRoleServiceHelper.CheckIfChatRoleExistsAndGetByIdAsync(newRoleId, false, cancellationToken);

            if (!_chatRoleServiceHelper.IsFirstRoleHasMorePrecedenceThenSecond(issuer.ChatRole, newRole))
                throw new NotEnoughRightsBadRequestException(issuer.Id);

            chatParticipantToUpdate.ChatRole = newRole;

            _chatParticipantRepository.Update(chatParticipantToUpdate);
            await _chatParticipantRepository.SaveChangesAsync();
        }
    }
}
