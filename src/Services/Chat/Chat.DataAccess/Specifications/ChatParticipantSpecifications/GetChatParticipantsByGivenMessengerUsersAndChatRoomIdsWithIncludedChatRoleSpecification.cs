using Chat.DataAccess.Models;

namespace Chat.DataAccess.Specifications.ChatParticipantSpecifications
{
    public class GetChatParticipantsByGivenMessengerUsersAndChatRoomIdsWithIncludedChatRoleSpecification : Specification<ChatParticipant>
    {
        public GetChatParticipantsByGivenMessengerUsersAndChatRoomIdsWithIncludedChatRoleSpecification(IEnumerable<Guid> messengerUsersIds, Guid chatRoomId)
            : base(participant => participant.ChatRoomId == chatRoomId && messengerUsersIds.Any(id => id == participant.Id))
        {
            AddInclude(chatParticipant => chatParticipant.ChatRole);
        }
    }
}
