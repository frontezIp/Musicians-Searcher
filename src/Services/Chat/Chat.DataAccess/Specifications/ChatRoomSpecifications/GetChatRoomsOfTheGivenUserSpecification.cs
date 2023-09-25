using Chat.DataAccess.Models;
using Chat.DataAccess.RequestParameters;

namespace Chat.DataAccess.Specifications.ChatRoomSpecifications
{
    public class GetChatRoomsOfTheGivenUserSpecification : Specification<ChatRoom>
    {
        public GetChatRoomsOfTheGivenUserSpecification(Guid userId,
            ChatRoomRequestParameters chatRoomRequestParameters)
            : base(chatRoom => chatRoom.ChatParticipants
            .Any(participant => participant.MessengerUserId == userId)
            && chatRoom.Title.ToLower().Contains(chatRoomRequestParameters.SearchTerm.ToLower()))
        {
            AddOrderByDescending(chatRoom => chatRoom.LastSentMessageAt);
            ApplyPaging(chatRoomRequestParameters.Skip, chatRoomRequestParameters.Take);
        }
    }
}
