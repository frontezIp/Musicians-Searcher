using Chat.DataAccess.Models;

namespace Chat.DataAccess.Specifications.ChatParticipantSpecifications
{
    public class GetChatParticipantByGivenUserAndChatRoomWithIncludedRoleSpecification : Specification<ChatParticipant>
    {
        public GetChatParticipantByGivenUserAndChatRoomWithIncludedRoleSpecification(Guid messengerUserId, Guid chatRoomId, bool trackChanges) 
            : base(chatParticipant => chatParticipant.MessengerUserId == messengerUserId && chatParticipant.ChatRoomId == chatRoomId)
        {
            AddInclude(chatParticipant => chatParticipant.ChatRole);
            if (trackChanges)
                AsTracking();
        }
    }
}
