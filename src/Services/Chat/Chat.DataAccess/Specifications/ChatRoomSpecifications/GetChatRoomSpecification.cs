using Chat.DataAccess.Models;

namespace Chat.DataAccess.Specifications.ChatRoomSpecifications
{
    public class GetChatRoomSpecification : Specification<ChatRoom>
    {
        public GetChatRoomSpecification(Guid chatRoomId, int lastMessagesNumberToInclude)
            : base(chatRoom => chatRoom.Id == chatRoomId)
        {
            AddInclude(chatRoom => chatRoom.Messages.TakeLast(lastMessagesNumberToInclude));
        }

    }
}
