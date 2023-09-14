using Chat.DataAccess.Models;

namespace Chat.DataAccess.Specifications.ChatParticipantSpecifications
{
    public class GetChatParticipantsWithIncludedChatRoleSpecification : Specification<ChatParticipant>
    {
        public GetChatParticipantsWithIncludedChatRoleSpecification(IEnumerable<Guid> chatParticipantsIds, bool trackChanges) 
            : base(chatParticipant => chatParticipantsIds.Any(id => id == chatParticipant.Id))
        {
            AddInclude(chatParticipant => chatParticipant.ChatRole);
            if (trackChanges)
                AsTracking();
        }
    }
}
