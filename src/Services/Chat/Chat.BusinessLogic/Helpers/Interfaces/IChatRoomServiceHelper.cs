using Chat.DataAccess.Models;

namespace Chat.BusinessLogic.Helpers.Interfaces
{
    public interface IChatRoomServiceHelper
    {
        Task<ChatRoom> CheckIfChatRoomExistsAndGetByIdAsync(Guid chatRoomId, bool trackChanges, CancellationToken cancellationToken);
        Task CheckIfChatRoomExistsAsync(Guid chatRoomId, CancellationToken cancellationToken);
    }
}
