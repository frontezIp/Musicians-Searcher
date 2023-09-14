using Chat.DataAccess.Models;

namespace Chat.DataAccess.Specifications.ChatParticipantSpecifications
{
    public class GetChatParticipantWithIncludedChatRoleSpecification : Specification<ChatParticipant>
    {
        public GetChatParticipantWithIncludedChatRoleSpecification(Guid chatParticipantId) 
            : base(chatParticipant => chatParticipant.Id == chatParticipantId)
        {
            AddInclude(chatParticipant => chatParticipant.ChatRole);
        }
    }
}
