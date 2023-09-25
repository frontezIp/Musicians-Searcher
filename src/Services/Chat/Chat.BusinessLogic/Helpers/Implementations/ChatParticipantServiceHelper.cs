using Chat.BusinessLogic.Helpers.Interfaces;
using Chat.DataAccess.Models;
using Chat.DataAccess.Repositories.Interfaces;
using Chat.BusinessLogic.Exceptions.NotFoundExceptions;
using Chat.BusinessLogic.Exceptions.BadRequestException;
using Chat.DataAccess.Specifications.ChatParticipantSpecifications;

namespace Chat.BusinessLogic.Helpers.Implementations
{
    internal class ChatParticipantServiceHelper : IChatParticipantServiceHelper
    {
        private readonly IChatParticipantRepository _chatParticipantReposiotory;

        public ChatParticipantServiceHelper(IChatParticipantRepository chatParticipantReposiotory)
        {
            _chatParticipantReposiotory = chatParticipantReposiotory;
        }

        public async Task<IEnumerable<ChatParticipant>> CheckIfChatParticipantsByGivenIdsExistsAndGetAsync(IEnumerable<Guid> chatParticipantsIds, bool trackChanges, CancellationToken cancellationToken)
        {
            if (chatParticipantsIds is null)
                throw new IdParameterBadRequestException(nameof(ChatParticipant));

            var chatParticipants = await _chatParticipantReposiotory.GetByIdsAsync(chatParticipantsIds, trackChanges, cancellationToken);

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
            var chatParticipant = await _chatParticipantReposiotory.GetByIdAsync(chatParticipantId, trackChanges, cancellationToken);

            if (chatParticipant == null)
                throw new ChatParticipantNotFoundException(chatParticipantId);

            if (chatParticipant.ChatRoomId != chatRoomId)
                throw new ChatParticipantNotFoundInTheGivenRoomException(chatRoomId, chatParticipantId);

            return chatParticipant;
        }

        public async Task<ChatParticipant> CheckIfChatParticipantExistsByGivenMessengerUserAndChatRoomIdAndGetAsync(Guid messengerUserId, Guid chatRoomId, bool trackChanges, CancellationToken cancellationToken)
        {
            var spec = new GetChatParticipantByGivenUserAndChatRoomWithIncludedRoleSpecification(messengerUserId, chatRoomId, trackChanges);

            var chatParticipant = await _chatParticipantReposiotory.GetSingleBySpecificationAsync(spec, cancellationToken);
            if (chatParticipant == null)
                throw new MessengerUserInTheGivenChatRoomNotFoundException(messengerUserId, chatRoomId);

            return chatParticipant;
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
