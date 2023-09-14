using Chat.BusinessLogic.Helpers.Interfaces;
using Chat.DataAccess.Models;
using Chat.DataAccess.Repositories.Interfaces;
using Chat.BusinessLogic.Exceptions.NotFoundExceptions;
using Chat.BusinessLogic.Exceptions.BadRequestException;

namespace Chat.BusinessLogic.Helpers.Implementations
{
    internal class MessengerUserServiceHelper : IMessengerUserServiceHelper
    {
        private readonly IMessengerUserRepository _messengerUserRepository;

        public MessengerUserServiceHelper(IMessengerUserRepository messengerUserRepository)
        {
            _messengerUserRepository = messengerUserRepository;
        }

        public async Task<MessengerUser> CheckIfMessengerUserExistsAndGetAsync(Guid messengerUserId, bool trackChanges, CancellationToken cancellationToken)
        {
            var user = await _messengerUserRepository.GetByIdAsync(messengerUserId, trackChanges, cancellationToken);

            if (user == null)
                throw new MessengerUserNotFoundException(messengerUserId);

            return user;
        }

        public async Task CheckIfMessengerUserExistsAsync(Guid messengerUserId, CancellationToken cancellationToken)
        {
            await CheckIfMessengerUserExistsAndGetAsync(messengerUserId, false, cancellationToken);
        }

        public async Task<IEnumerable<MessengerUser>> CheckIfMessengerUsersByGivenIdsExistsAndGetAsync(IEnumerable<Guid> messengerUserIds, bool trackChanges, CancellationToken cancellationToken)
        {
            if (messengerUserIds is null)
                throw new IdParameterBadRequestException(nameof(MessengerUser));

            var messengerUsers = await _messengerUserRepository.GetByIdsAsync(messengerUserIds, trackChanges, cancellationToken);

            if (messengerUsers.Count() != messengerUserIds.Count())
                throw new CollectionByIdsBadRequestException(nameof(MessengerUser));

            return messengerUsers;
        }
    }
}
