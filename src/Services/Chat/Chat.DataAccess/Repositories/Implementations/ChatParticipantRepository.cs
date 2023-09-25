using Chat.DataAccess.Contexts;
using Chat.DataAccess.Models;
using Chat.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccess.Repositories.Implementations
{
    internal class ChatParticipantRepository : RepositoryBase<ChatParticipant>, IChatParticipantRepository
    {
        public ChatParticipantRepository(ChatContext context) : base(context)
        {
        }

        public async Task<bool> IsAnyOfTheGivenUsersAlreadyChatParticipant(Guid chatRoomId, IEnumerable<Guid> messengerUserIds, CancellationToken cancellationToken)
        {
            var result = await GetByCondition(participant => participant.ChatRoomId == chatRoomId, false)
                            .AnyAsync(participant => messengerUserIds.Contains(participant.MessengerUserId), cancellationToken);

            return result;
        }

        public async Task<bool> IsAllGivenChatParticipantsInTheGivenChatRoom(Guid chatRoomId, IEnumerable<Guid> chatParticipantsIds, CancellationToken cancellationToken)
        {
            var result = await GetByCondition(participant => participant.ChatRoomId == chatRoomId && chatParticipantsIds.Any(id => id == participant.Id), false)
                            .CountAsync(cancellationToken);
           
            return result == chatParticipantsIds.Count();
        }
    }
}
