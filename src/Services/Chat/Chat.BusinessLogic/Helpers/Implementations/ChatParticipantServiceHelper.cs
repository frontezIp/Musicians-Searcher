using Chat.BusinessLogic.Helpers.Interfaces;
using Chat.DataAccess.Models;
using Chat.DataAccess.Repositories.Interfaces;
using Chat.BusinessLogic.Exceptions.NotFoundExceptions;
using Chat.BusinessLogic.Exceptions.BadRequestException;
using Chat.DataAccess.Specifications.ChatParticipantSpecifications;
using Chat.BusinessLogic.Grpc.v1.Clients.Interfaces;

namespace Chat.BusinessLogic.Helpers.Implementations
{
    internal class ChatParticipantServiceHelper : IChatParticipantServiceHelper
    {
        private readonly IChatParticipantRepository _chatParticipantRepository;
        private readonly IMusicianClient _musicianClient;

        public ChatParticipantServiceHelper(IChatParticipantRepository chatParticipantReposiotory,
            IMusicianClient musicianClient)
        {
            _chatParticipantRepository = chatParticipantReposiotory;
            _musicianClient = musicianClient;
        }

        public async Task<IEnumerable<ChatParticipant>> CheckIfChatParticipantsByGivenIdsExistsAndGetAsync(IEnumerable<Guid> chatParticipantsIds, bool trackChanges, CancellationToken cancellationToken)
        {
            if (chatParticipantsIds is null)
                throw new IdParameterBadRequestException(nameof(ChatParticipant));

            var chatParticipants = await _chatParticipantRepository.GetByIdsAsync(chatParticipantsIds, trackChanges, cancellationToken);

            if (chatParticipants.Count() != chatParticipantsIds.Count())
                throw new CollectionByIdsBadRequestException(nameof(ChatParticipant));

            return chatParticipants;
        }

        public async Task CheckIfChatParticipantsExistsAsync(IEnumerable<Guid> chatParticipantsIds, CancellationToken cancellationToken)
        {
            await CheckIfChatParticipantsByGivenIdsExistsAndGetAsync(chatParticipantsIds, false, cancellationToken);
        }

        public async Task<ChatParticipant> CheckIfChatParticipantExistsAndGetAsync(Guid chatRoomId, Guid chatParticipantId, bool trackChanges, CancellationToken cancellationToken)
        {
            var chatParticipant = await _chatParticipantRepository.GetByIdAsync(chatParticipantId, trackChanges, cancellationToken);

            if (chatParticipant == null)
                throw new ChatParticipantNotFoundException(chatParticipantId);

            if (chatParticipant.ChatRoomId != chatRoomId)
                throw new ChatParticipantNotFoundInTheGivenRoomException(chatRoomId, chatParticipantId);

            return chatParticipant;
        }

        public async Task<ChatParticipant> CheckIfChatParticipantExistsByGivenMessengerUserAndChatRoomIdAndGetAsync(Guid messengerUserId, Guid chatRoomId, bool trackChanges, CancellationToken cancellationToken)
        {
            var spec = new GetChatParticipantByGivenUserAndChatRoomWithIncludedRoleSpecification(messengerUserId, chatRoomId, trackChanges);

            var chatParticipant = await _chatParticipantRepository.GetSingleBySpecificationAsync(spec, cancellationToken);
            if (chatParticipant == null)
                throw new MessengerUserInTheGivenChatRoomNotFoundException(messengerUserId, chatRoomId);

            return chatParticipant;
        }

        public async Task<bool> CheckIfAllAreFriendsAsync(Guid issuerId, IEnumerable<Guid> friendsIdsToCheck)
        {
            var friends = await _musicianClient.GetMessengerUserFriendsAsync(issuerId.ToString());

            var friendsIds = friends.Friends.Select(friend => friend.Id).ToList();

            if (friendsIdsToCheck.Any(possibleFriendId => !friendsIds.Contains(possibleFriendId.ToString())))
            {
                return false;
            }

            return true;
        }

        public async Task CheckIfChatParticipantExistsByGivenMessengerUserAndChatRoomIdAsync(Guid messengerUserId, Guid chatRoomId, CancellationToken cancellationToken)
        {
            await CheckIfChatParticipantExistsByGivenMessengerUserAndChatRoomIdAndGetAsync(messengerUserId, chatRoomId, false, cancellationToken);
        }

        public async Task CheckIfChatParticipantExistsAsync(Guid chatRoomid, Guid chatParticipantId, CancellationToken cancellationToken)
        {
            await CheckIfChatParticipantExistsAndGetAsync(chatRoomid, chatParticipantId, false, cancellationToken);
        }
    }
}
