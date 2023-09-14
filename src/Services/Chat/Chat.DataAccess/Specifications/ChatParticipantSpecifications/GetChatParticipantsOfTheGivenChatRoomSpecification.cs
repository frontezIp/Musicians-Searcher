using Chat.DataAccess.Models;
using Chat.DataAccess.RequestParameters;

namespace Chat.DataAccess.Specifications.ChatParticipantSpecifications
{
    public class GetChatParticipantsOfTheGivenChatRoomSpecification : Specification<ChatParticipant>
    {
        public GetChatParticipantsOfTheGivenChatRoomSpecification(Guid chatRoomId,
            ChatParticipantRequestParameters chatParticipantRequestParameters) 
            : base(chatParticipant => chatParticipant.ChatRoomId == chatRoomId)
        {
            AddInclude(chatParticipant => chatParticipant.ChatRole);
            AddInclude(chatParticipant => chatParticipant.MessengerUser);
            ApplyPaging(chatParticipantRequestParameters.Skip, chatParticipantRequestParameters.Take);
        }
    }
}
