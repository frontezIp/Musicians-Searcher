using Chat.BusinessLogic.DTOs.RequestDTOs.MessageRequestsDTOs;
using Chat.BusinessLogic.HangfireServices.Interfaces;
using Chat.BusinessLogic.Helpers.Interfaces;
using Chat.DataAccess.Models;
using Chat.DataAccess.Repositories.Interfaces;
using MapsterMapper;

namespace Chat.BusinessLogic.HangfireServices.Implementations
{
    internal class HangfireMessageService : IHangfireMessageService
    {
        private readonly IChatRoomServiceHelper _chatRoomServiceHelper;
        private readonly IMessengerUserServiceHelper _messengerUserServiceHelper;
        private readonly IChatParticipantServiceHelper _chatParticipantServiceHelper;
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IMapper _mapper;

        public HangfireMessageService(IChatRoomServiceHelper chatRoomServiceHelper,
            IMessengerUserServiceHelper messengerUserServiceHelper,
            IChatParticipantServiceHelper chatParticipantServiceHelper,
            IMessageRepository messageRepository,
            IMapper mapper,
            IChatRoomRepository chatRoomRepository)
        {
            _chatRoomServiceHelper = chatRoomServiceHelper;
            _messengerUserServiceHelper = messengerUserServiceHelper;
            _chatParticipantServiceHelper = chatParticipantServiceHelper;
            _messageRepository = messageRepository;
            _mapper = mapper;
            _chatRoomRepository = chatRoomRepository;
        }

        public async Task SendDelayedMessageAsync(Guid messengerUserId,
            Guid chatRoomId,
            CreateMessageRequestDto createMessageRequestDto,
            CancellationToken cancellationToken = default)
        {
            await _messengerUserServiceHelper.CheckIfMessengerUserExistsAndGetAsync(messengerUserId, false, cancellationToken);
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
    }
}
