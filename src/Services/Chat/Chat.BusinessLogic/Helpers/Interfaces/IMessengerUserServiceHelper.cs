using Chat.DataAccess.Models;

namespace Chat.BusinessLogic.Helpers.Interfaces
{
    public interface IMessengerUserServiceHelper
    {
        Task CheckIfMessengerUserExistsAsync(Guid messengerUserId, CancellationToken cancellationToken);
        Task<MessengerUser> CheckIfMessengerUserExistsAndGetAsync(Guid messengerUserId, bool trackChanges, CancellationToken cancellationToken);
        Task<IEnumerable<MessengerUser>> CheckIfMessengerUsersByGivenIdsExistsAndGetAsync(IEnumerable<Guid> messengerUserIds, bool trackChanges, CancellationToken cancellationToken);
    }
}
