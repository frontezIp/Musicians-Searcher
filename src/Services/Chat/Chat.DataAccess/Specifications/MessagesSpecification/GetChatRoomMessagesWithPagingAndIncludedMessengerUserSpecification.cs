using Chat.DataAccess.Models;
using Chat.DataAccess.RequestParameters;

namespace Chat.DataAccess.Specifications.MessagesSpecification
{
    public class GetChatRoomMessagesWithPagingAndIncludedMessengerUserSpecification : Specification<Message>
    {
        public GetChatRoomMessagesWithPagingAndIncludedMessengerUserSpecification(Guid chatRoomId, MessagesRequestParameters messagesRequestParameters) 
            : base(message => message.ChatRoomId == chatRoomId)
        {
            AddInclude(message => message.MessengerUser);
            AddOrderByDescending(message => message.CreatedAt);
            ApplyPaging(messagesRequestParameters.Skip, messagesRequestParameters.Take);
        }
    }
}
