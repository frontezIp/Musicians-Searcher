namespace Chat.BusinessLogic.Exceptions.BadRequestException
{
    public class AddUnfamiliarMessengerUsersInTheChatRoomBadRequestException : BadRequestException
    {
        public AddUnfamiliarMessengerUsersInTheChatRoomBadRequestException(Guid issuerId, Guid chatRoomId) 
            : base($"Some of the users that user with {issuerId} id tried to add in the chat room with {chatRoomId} are not in his friends list")
        {
        }
    }
}
