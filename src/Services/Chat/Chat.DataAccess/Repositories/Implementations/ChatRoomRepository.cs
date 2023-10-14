using Chat.DataAccess.Contexts;
using Chat.DataAccess.Models;
using Chat.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccess.Repositories.Implementations
{
    internal class ChatRoomRepository : RepositoryBase<ChatRoom>, IChatRoomRepository
    {
        public ChatRoomRepository(ChatContext context) : base(context)
        {
        }

        public async Task DeleteEmptyChatRoomsAsync(CancellationToken cancellationToken = default)
        {
            var chatRooms = await GetByCondition(chatRoom => !chatRoom.ChatParticipants.Any(), true).ToListAsync(cancellationToken);
            DeleteMany(chatRooms);
            await SaveChangesAsync(cancellationToken);
        }
    }
}
