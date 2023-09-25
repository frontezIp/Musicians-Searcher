using Chat.BusinessLogic.DTOs.RequestDTOs.MessageRequestsDTOs;
using Chat.BusinessLogic.Helpers.Interfaces;
using Chat.BusinessLogic.Services.Interfaces;
using Chat.DataAccess.Models;
using Chat.DataAccess.Repositories.Interfaces;
using MapsterMapper;
using Chat.BusinessLogic.Exceptions.BadRequestException;
using Chat.DataAccess.Specifications.ChatParticipantSpecifications;

namespace Chat.BusinessLogic.Services.Implementations
{
    internal class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMessageServiceHelper _messageServiceHelper;
        private readonly IChatParticipantServiceHelper _chatParticipantServiceHelper;
        private readonly IChatRoomServiceHelper _chatRoomServiceHelper;
        private readonly IMessengerUserServiceHelper _messengerUserServiceHelper;
        private readonly IChatRoleServiceHelper _chatRoleServiceHelper;
        private readonly IChatParticipantRepository _chatParticipantRepository;
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IMapper _mapper;

        public MessageService(IMessageRepository messageRepository,
            IChatParticipantServiceHelper chatParticipantServiceHelper,
            IChatRoomServiceHelper chatRoomServiceHelper,
            IMessengerUserServiceHelper messengerUserServiceHelper,
            IChatRoleServiceHelper chatRoleServiceHelper,
            IMapper mapper,
            IMessageServiceHelper messageServiceHelper,
            IChatParticipantRepository chatParticipantRepository,
            IChatRoomRepository chatRoomRepository)
        {
            _messageRepository = messageRepository;
            _chatParticipantServiceHelper = chatParticipantServiceHelper;
            _chatRoomServiceHelper = chatRoomServiceHelper;
            _messengerUserServiceHelper = messengerUserServiceHelper;
            _chatRoleServiceHelper = chatRoleServiceHelper;
            _mapper = mapper;
            _messageServiceHelper = messageServiceHelper;
            _chatParticipantRepository = chatParticipantRepository;
            _chatRoomRepository = chatRoomRepository;
        }

        public async Task CreateMessageAsync(Guid messengerUserId, Guid chatRoomId, CreateMessageRequestDto createMessageRequestDto, CancellationToken cancellationToken = default)
        {
            await _messengerUserServiceHelper.CheckIfMessengerUserExistsAsync(messengerUserId, cancellationToken);
            var chatRoom = await _chatRoomServiceHelper.CheckIfChatRoomExistsAndGetByIdAsync(chatRoomId, true, cancellationToken);
            await _chatParticipantServiceHelper.CheckIfChatParticipantExistsByGivenMessengerUserAndChatRoomIdAsync(messengerUserId, chatRoomId, cancellationToken);

            var newMessage = _mapper.Map<Message>(createMessageRequestDto);
            newMessage.ChatRoomId = chatRoomId;
            newMessage.MessengerUserId = messengerUserId;

            _messageRepository.Create(newMessage);

            chatRoom.LastSentMessageAt = newMessage.CreatedAt;
            chatRoom.MessagesCount += 1;
            _chatRoomRepository.Update(chatRoom);

            await _chatRoomRepository.SaveChangesAsync();
        }

        public async Task DeleteMessagesAsync(Guid messengerUserId, Guid chatRoomId, IEnumerable<Guid> messagesToDeleteIds, CancellationToken cancellationToken = default)
        {
            await _messengerUserServiceHelper.CheckIfMessengerUserExistsAsync(messengerUserId, cancellationToken);
            var chatRoom = await _chatRoomServiceHelper.CheckIfChatRoomExistsAndGetByIdAsync(chatRoomId, true, cancellationToken);
            var chatParticipant = await _chatParticipantServiceHelper.CheckIfChatParticipantExistsByGivenMessengerUserAndChatRoomIdAndGetAsync(messengerUserId, chatRoomId, false, cancellationToken);
            var messages = await _messageServiceHelper.CheckIfMessagesByIdsExistsAndGetAsync(messagesToDeleteIds, true, cancellationToken);

            var chatRooms = messages.Select(message => message.ChatRoomId).Distinct().ToList();

            if (chatRooms.Count() > 1 || chatRooms.SingleOrDefault() != chatRoomId)
            {
                throw new DeleteMessagesFromMultipleChatRoomsBadRequestException();
            }

            if (!await _messageRepository.IsAllGivenMessagesBelongToTheGivenUser(messagesToDeleteIds, messengerUserId, cancellationToken))
            {
                var messengerUsersIds = messages.Select(message => message.MessengerUserId).ToList();

                var spec = new GetChatParticipantsByGivenMessengerUsersAndChatRoomIdsWithIncludedChatRoleSpecification(messengerUsersIds, chatRoomId);
                var chatParticipants = await _chatParticipantRepository.GetBySpecificationAsync(spec, cancellationToken);

                var result = chatParticipants
                    .Where(chatParticipants => chatParticipants.MessengerUserId != messengerUserId)
                    .Any(chatParticipants => !_chatRoleServiceHelper.IsFirstRoleHasMorePrecedenceThenSecond(chatParticipant.ChatRole, chatParticipants.ChatRole));

                if (result)
                {
                    throw new DeleteMessagesOfTheUsersWithTheSameOrHigherPrivilegesBadRequestException(messengerUserId);
                }
            }

            _messageRepository.DeleteMany(messages);
            chatRoom.MessagesCount -= messages.Count();

            if (chatRoom.MessagesCount == 0)
            {
                chatRoom.LastSentMessageAt = chatRoom.CreatedAt.ToDateTime(new TimeOnly(0,0,0));
            }
            else
            {

                var lastMessage = await _messageRepository.GetLastChatRoomMessage(chatRoomId, false, cancellationToken);
                chatRoom.LastSentMessageAt = lastMessage!.CreatedAt;
            }

            _chatRoomRepository.Update(chatRoom);

            await _chatRoomRepository.SaveChangesAsync();
        }

        public async Task UpdateMessageAsync(Guid messengerUserId, Guid chatRoomId, Guid messageId, UpdateMessageRequestDto updateMessageRequestDto, CancellationToken cancellationToken = default)
        {
            await _messengerUserServiceHelper.CheckIfMessengerUserExistsAsync(messengerUserId, cancellationToken);
            await _chatRoomServiceHelper.CheckIfChatRoomExistsAsync(chatRoomId, cancellationToken);
            await _chatParticipantServiceHelper.CheckIfChatParticipantExistsByGivenMessengerUserAndChatRoomIdAsync(messengerUserId, chatRoomId, cancellationToken);
            var message = await _messageServiceHelper.CheckIfMessageExistsByIdAndGetAsync(messageId, true, cancellationToken);

            if (message.MessengerUserId != messengerUserId) 
            {
                throw new UpdateAnotherUserMessageBadRequestException(messengerUserId);
            }

            if (message.ChatRoomId != chatRoomId)
            {
                throw new UpdateMessageFromAnotherChatRoomException(chatRoomId);
            }

            var updatedMessage = _mapper.Map(updateMessageRequestDto, message);

            _messageRepository.Update(updatedMessage);

            await _messageRepository.SaveChangesAsync();
        }
    }
}
