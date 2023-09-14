using Chat.DataAccess.Models;
using System.Linq.Expressions;

namespace Chat.DataAccess.Specifications.ChatParticipantSpecifications
{
    public class GetChatParticipantWithIncludedChat : Specification<ChatParticipant>
    {
        public GetChatParticipantWithIncludedChat(Guid chatParticipantId) 
            : base(chatParticipant => chatParticipant.Id == chatParticipantId)
        {
            AddInclude(chatParticipant => chatParticipant.ChatRole);
        }
    }
}
