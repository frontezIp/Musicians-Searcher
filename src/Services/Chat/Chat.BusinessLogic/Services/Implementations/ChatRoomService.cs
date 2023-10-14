using Chat.BusinessLogic.DTOs.RequestDTOs;
using Chat.BusinessLogic.DTOs.RequestDTOs.ChatRoomRequestsDTOs;
using Chat.BusinessLogic.DTOs.RequestDTOs.MessageRequestsDTOs;
using Chat.BusinessLogic.DTOs.ResponseDTOs;
using Chat.BusinessLogic.Exceptions.BadRequestException;
using Chat.BusinessLogic.Extensions;
using Chat.BusinessLogic.Helpers.Interfaces;
using Chat.BusinessLogic.RequestFeatures;
using Chat.BusinessLogic.Services.Interfaces;
using Chat.DataAccess.Models;
using Chat.DataAccess.Options;
using Chat.DataAccess.Repositories.Interfaces;
using Chat.DataAccess.RequestParameters;
using Chat.DataAccess.Specifications.ChatRoomSpecifications;
using Chat.DataAccess.Specifications.MessagesSpecification;
using MapsterMapper;
using Microsoft.Extensions.Options;

namespace Chat.BusinessLogic.Services.Implementations
{
    internal class ChatRoomService : IChatRoomService
    {
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IMessengerUserServiceHelper _messengerUserServiceHelper;
        private readonly IMapper _mapper;
        private readonly IOptions<ChatRolesOptions> _chatRoleOptions;
        private readonly IChatRoleServiceHelper _chatRoleServiceHelper;
        private readonly IChatParticipantRepository _chatParticipantRepository;
        private readonly IChatRoomServiceHelper _chatRoomServiceHelper;
        private readonly IChatParticipantServiceHelper _chatParticipantServiceHelper;
        private readonly IMessageRepository _messagesRepository;

        public ChatRoomService(IChatRoomRepository chatRoomRepository,
            IMessengerUserServiceHelper messengerUserServiceHelper,
            IMapper mapper,
            IOptions<ChatRolesOptions> chatRoleOptions,
            IChatRoleServiceHelper chatRoleServiceHelper,
            IChatParticipantRepository chatParticipantRepository,
            IChatRoomServiceHelper chatRoomServiceHelper,
            IChatParticipantServiceHelper chatParticipantServiceHelper,
            IMessageRepository messagesRepository)
        {
            _chatRoomRepository = chatRoomRepository;
            _messengerUserServiceHelper = messengerUserServiceHelper;
            _mapper = mapper;
            _chatRoleOptions = chatRoleOptions;
            _chatRoleServiceHelper = chatRoleServiceHelper;
            _chatParticipantRepository = chatParticipantRepository;
            _chatRoomServiceHelper = chatRoomServiceHelper;
            _chatParticipantServiceHelper = chatParticipantServiceHelper;
            _messagesRepository = messagesRepository;
        }

        public async Task<(IEnumerable<ChatRoomResponseDto> chatRooms, MetaData metaData)> GetChatRoomsOfGivenMessengerUserAsync(Guid messengerUserId,
            PaginatedUserChatRoomsRequestDto paginatedUserChatRoomsRequestDto,
            CancellationToken cancellationToken = default)
        {
            await _messengerUserServiceHelper.CheckIfMessengerUserExistsAsync(messengerUserId, cancellationToken);

            var specParameters = _mapper.Map<ChatRoomRequestParameters>(paginatedUserChatRoomsRequestDto);
            var spec = new GetChatRoomsOfTheGivenUserSpecification(messengerUserId, specParameters);

            var chatRooms = await _chatRoomRepository.GetBySpecificationAsync(spec, cancellationToken);
            var chatRoomsDtos = _mapper.Map<IEnumerable<ChatRoomResponseDto>>(chatRooms);
            var count = chatRooms.Count();

            var metadata = chatRooms.GetMetaData(count, paginatedUserChatRoomsRequestDto.PageNumber, paginatedUserChatRoomsRequestDto.PageSize);

            return (chatRooms: chatRoomsDtos, metaData: metadata);
        }

        public async Task<ChatRoomResponseDto> CreateChatRoomAsync(Guid messengerUserId, CreateChatRoomRequestDto chatRoomRequestDto, CancellationToken cancellationToken = default)
        {
            var creator = await _messengerUserServiceHelper.CheckIfMessengerUserExistsAndGetAsync(messengerUserId, false, cancellationToken);
            var possibleChatParticipants = await _messengerUserServiceHelper.CheckIfMessengerUsersByGivenIdsExistsAndGetAsync(chatRoomRequestDto.MessengerUsersIdsToAdd,
                false,
                cancellationToken);

            if (chatRoomRequestDto.MessengerUsersIdsToAdd.Count > 0)
            {
                var areAllFriends = await _chatParticipantServiceHelper.CheckIfAllAreFriendsAsync(messengerUserId,
                    chatRoomRequestDto.MessengerUsersIdsToAdd);

                if (!areAllFriends)
                {
                    throw new CreateChatRoomWithUnfamiliarMessengerUsersBadRequestException(messengerUserId);
                }
            }

            var creatorChatRole = await _chatRoleServiceHelper.CheckIfChatRoleExistsAndGetByNameAsync(_chatRoleOptions.Value.Creator, false, cancellationToken);
            var userChatRole = await _chatRoleServiceHelper.CheckIfChatRoleExistsAndGetByNameAsync(_chatRoleOptions.Value.User, false, cancellationToken);

            var possibleChatRoom = _mapper.Map<ChatRoom>(chatRoomRequestDto);
            possibleChatRoom.CreatorId = creator.Id;
            possibleChatRoom.MembersNumber = 1 + possibleChatParticipants.Count();

            var creatorChatParticipant = new ChatParticipant() { ChatRoleId = creatorChatRole.Id, ChatRoom = possibleChatRoom, MessengerUserId = creator.Id };

            var chatParticipants = possibleChatParticipants.Select(possibleChatParticipant => new ChatParticipant
            {
                ChatRoleId = userChatRole.Id,
                MessengerUserId = possibleChatParticipant.Id,
                ChatRoom = possibleChatRoom
            }).ToList();

            chatParticipants.Add(creatorChatParticipant);

            _chatRoomRepository.Create(possibleChatRoom);
            await _chatRoomRepository.SaveChangesAsync();

            await _chatParticipantRepository.AddManyAsync(chatParticipants);
            await _chatParticipantRepository.SaveChangesAsync();

            var chatRoomDtoToReturn = _mapper.Map<ChatRoomResponseDto>(possibleChatRoom);

            return chatRoomDtoToReturn;
        }

        public async Task UpdateChatRoomAsync(Guid messengerUserId, Guid chatRoomId, UpdateChatRoomRequestDto chatRoomUpdateRequestDto, CancellationToken cancellationToken = default)
        {
            await _messengerUserServiceHelper.CheckIfMessengerUserExistsAsync(messengerUserId, cancellationToken);
            var chatRoom = await _chatRoomServiceHelper.CheckIfChatRoomExistsAndGetByIdAsync(chatRoomId, true, cancellationToken);

            var chatParticipant = await _chatParticipantServiceHelper.CheckIfChatParticipantExistsByGivenMessengerUserAndChatRoomIdAndGetAsync(messengerUserId, chatRoomId, false, cancellationToken);

            if (!(chatParticipant.ChatRole.Name == _chatRoleOptions.Value.Admin
                || chatParticipant.ChatRole.Name == _chatRoleOptions.Value.Creator))
                throw new NotEnoughRightsBadRequestException(chatParticipant.Id);

            var updatedChatRoom = _mapper.Map(chatRoomUpdateRequestDto, chatRoom);  

            _chatRoomRepository.Update(updatedChatRoom);
            await _chatRoomRepository.SaveChangesAsync();
        }

        public async Task<ChatRoomResponseDto> GetChatRoomAsync(Guid messengerUserId, Guid chatRoomId, GetMessagesRequestDto getMessagesRequestDto, CancellationToken cancellationToken = default)
        {
            await _messengerUserServiceHelper.CheckIfMessengerUserExistsAsync(messengerUserId, cancellationToken);
            var chatRoom = await _chatRoomServiceHelper.CheckIfChatRoomExistsAndGetByIdAsync(chatRoomId, false, cancellationToken);
            await _chatParticipantServiceHelper.CheckIfChatParticipantExistsByGivenMessengerUserAndChatRoomIdAsync(messengerUserId, chatRoomId, cancellationToken);

            var messagesRequestParameters = _mapper.Map<MessagesRequestParameters>(getMessagesRequestDto);

            var spec = new GetChatRoomMessagesWithPagingAndIncludedMessengerUserSpecification(chatRoomId, messagesRequestParameters);
            var chatRoomMessages = await _messagesRepository.GetBySpecificationAsync(spec, cancellationToken);

            var chatRoomDto = _mapper.Map<ChatRoomResponseDto>(chatRoom);
            var chatRoomMessagesDtos = _mapper.Map<IEnumerable<MessageResponseDto>>(chatRoomMessages);
            chatRoomDto.Messages = chatRoomMessagesDtos.ToList();

            return chatRoomDto;
        }
    }
}
