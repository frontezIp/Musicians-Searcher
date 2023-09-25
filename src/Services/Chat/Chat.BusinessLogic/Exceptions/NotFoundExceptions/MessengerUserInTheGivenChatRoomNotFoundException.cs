namespace Chat.BusinessLogic.Exceptions.NotFoundExceptions
{
    public class MessengerUserInTheGivenChatRoomNotFoundException
        : NotFoundException
    {
        public MessengerUserInTheGivenChatRoomNotFoundException(Guid messengerUserId, Guid chatRoomId)
            : base($"Messenger user with {messengerUserId} id not exists in the chat room with {chatRoomId} id as a chat participant")
        {
        }
    }
}
