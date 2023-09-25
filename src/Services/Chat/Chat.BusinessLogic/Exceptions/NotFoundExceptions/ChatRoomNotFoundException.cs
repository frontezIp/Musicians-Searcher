namespace Chat.BusinessLogic.Exceptions.NotFoundExceptions
{
    public class ChatRoomNotFoundException : NotFoundException
    {
        public ChatRoomNotFoundException(Guid chatRoomId)
            : base($"Chat room with {chatRoomId} does not exists")
        {
        }
    }
}
