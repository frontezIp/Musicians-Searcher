using Chat.DataAccess.Models;

namespace Chat.BusinessLogic.Helpers.Interfaces
{
    public interface IMessageServiceHelper
    {
        Task<Message> CheckIfMessageExistsByIdAndGetAsync(Guid messageId, bool trackChanges, CancellationToken cancellationToken = default);
        Task<IEnumerable<Message>> CheckIfMessagesByIdsExistsAndGetAsync(IEnumerable<Guid> ids, bool trackChanges, CancellationToken cancellationToken = default);
    }
}
