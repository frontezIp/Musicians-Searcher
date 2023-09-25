using Chat.DataAccess.Models;
using Chat.DataAccess.RequestParameters;

namespace Chat.DataAccess.Specifications.ChatParticipantSpecifications
{
    public class GetChatParticipantsOfTheGivenChatRoomWithPagingSpecification : Specification<ChatParticipant>
    {
        public GetChatParticipantsOfTheGivenChatRoomWithPagingSpecification(Guid chatRoomId,
            ChatParticipantRequestParameters chatParticipantRequestParameters) 
            : base(chatParticipant => chatParticipant.ChatRoomId == chatRoomId)
        {
            AddInclude(chatParticipant => chatParticipant.ChatRole);
            AddInclude(chatParticipant => chatParticipant.MessengerUser);
            AddOrderByDescending(chatParticipant => chatParticipant.MessengerUser.Username);
            ApplyPaging(chatParticipantRequestParameters.Skip, chatParticipantRequestParameters.Take);
        }
    }
}
