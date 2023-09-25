using Chat.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DataAccess.Repositories.Interfaces
{
    public interface IMessageRepository : IRepositoryBase<Message>
    {
        Task<Message?> GetLastChatRoomMessage(Guid chatRoomId, bool trackChanges, CancellationToken cancellationToken = default);
        Task<bool> IsAllGivenMessagesBelongToTheGivenUser(IEnumerable<Guid> messagesIds, Guid messengerUserId, CancellationToken cancellationToken = default);
    }
}
