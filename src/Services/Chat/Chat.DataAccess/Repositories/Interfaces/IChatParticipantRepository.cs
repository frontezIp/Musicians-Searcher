using Chat.DataAccess.Models;

namespace Chat.DataAccess.Repositories.Interfaces
{
    public interface IChatParticipantRepository : IRepositoryBase<ChatParticipant>
    {
        Task<bool> IsAllGivenChatParticipantsInTheGivenChatRoom(Guid chatRoomId, IEnumerable<Guid> chatParticipantsIds, CancellationToken cancellationToken);
        Task<bool> IsAnyOfTheGivenUsersAlreadyChatParticipant(Guid chatRoomId, IEnumerable<Guid> messengerUserIds, CancellationToken cancellationToken);
    }
}
