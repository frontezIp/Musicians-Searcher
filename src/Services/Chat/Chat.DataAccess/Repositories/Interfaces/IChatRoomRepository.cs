using Chat.DataAccess.Models;

namespace Chat.DataAccess.Repositories.Interfaces
{
    public interface IChatRoomRepository : IRepositoryBase<ChatRoom>
    {
        Task DeleteEmptyChatRoomsAsync(CancellationToken cancellationToken);
    }
}
