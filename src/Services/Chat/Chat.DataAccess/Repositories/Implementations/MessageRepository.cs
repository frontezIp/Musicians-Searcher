using Chat.DataAccess.Contexts;
using Chat.DataAccess.Models;
using Chat.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccess.Repositories.Implementations
{
    internal class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        public MessageRepository(ChatContext context) : base(context)
        {
        }

        public async Task<bool> IsAllGivenMessagesBelongToTheGivenUser(IEnumerable<Guid> messagesIds, Guid messengerUserId, CancellationToken cancellationToken = default)
        {
            var count = await GetByCondition(message => message.MessengerUserId == messengerUserId && messagesIds.Any(id => id == message.Id), false)
                .CountAsync(cancellationToken);

            return count == messagesIds.Count();
        }

        public async Task<Message?> GetLastChatRoomMessage(Guid chatRoomId, bool trackChanges, CancellationToken cancellationToken = default)
        {
            return await GetByCondition(message => message.ChatRoomId == chatRoomId, trackChanges)
                .OrderByDescending(message => message.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
