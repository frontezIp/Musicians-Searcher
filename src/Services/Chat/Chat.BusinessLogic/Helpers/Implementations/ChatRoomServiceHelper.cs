using Chat.BusinessLogic.Exceptions.NotFoundExceptions;
using Chat.BusinessLogic.Helpers.Interfaces;
using Chat.DataAccess.Models;
using Chat.DataAccess.Repositories.Interfaces;

namespace Chat.BusinessLogic.Helpers.Implementations
{
    public class ChatRoomServiceHelper : IChatRoomServiceHelper
    {
        private readonly IChatRoomRepository _chatRoomRepository;

        public ChatRoomServiceHelper(IChatRoomRepository chatRoomRepository)
        {
            _chatRoomRepository = chatRoomRepository;
        }

        public async Task<ChatRoom> CheckIfChatRoomExistsAndGetByIdAsync(Guid chatRoomId, bool trackChanges, CancellationToken cancellationToken)
        {
            var chatRoom = await _chatRoomRepository.GetByIdAsync(chatRoomId, trackChanges, cancellationToken);

            if (chatRoom == null)
                throw new ChatRoomNotFoundException(chatRoomId);

            return chatRoom;
        }

        public async Task CheckIfChatRoomExistsAsync(Guid chatRoomId, CancellationToken cancellationToken)
        {
            await CheckIfChatRoomExistsAndGetByIdAsync(chatRoomId, false, cancellationToken);
        }
    }
}
