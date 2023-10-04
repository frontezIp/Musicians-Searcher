namespace Chat.BusinessLogic.Exceptions.BadRequestException
{
    public class CreateChatRoomWithUnfamiliarMessengerUsersBadRequestException : BadRequestException
    {
        public CreateChatRoomWithUnfamiliarMessengerUsersBadRequestException(Guid issuerId)
            : base($"User with {issuerId} id tried to create chat room with unfamiliar users")
        {
        }
    }
}
