using Chat.DataAccess.Models;

namespace Chat.BusinessLogic.Helpers.Interfaces
{
    public interface IChatParticipantServiceHelper
    {
        Task<ChatParticipant> CheckIfChatParticipantExistsAndGetAsync(Guid chatRoomid, Guid chatParticipantId, bool trackChanges, CancellationToken cancellationToken);
        Task CheckIfChatParticipantExistsAsync(Guid chatRoomId, Guid chatParticipantId, CancellationToken cancellationToken);
        Task<ChatParticipant> CheckIfChatParticipantExistsByGivenMessengerUserAndChatRoomIdAndGetAsync(Guid messengerUserId, Guid chatRoomId, bool trackChanges, CancellationToken cancellationToken);
        Task CheckIfChatParticipantExistsByGivenMessengerUserAndChatRoomIdAsync(Guid messengerUserId, Guid chatRoomId, CancellationToken cancellationToken);
        Task<IEnumerable<ChatParticipant>> CheckIfChatParticipantsByGivenIdsExistsAndGetAsync(IEnumerable<Guid> chatParticipantsIds, bool trackChanges, CancellationToken cancellationToken);
        Task CheckIfChatParticipantsExistsAsync(IEnumerable<Guid> chatParticipantsIds, CancellationToken cancellationToken);
    }
}
