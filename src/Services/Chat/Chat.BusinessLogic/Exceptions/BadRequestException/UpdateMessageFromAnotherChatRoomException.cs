namespace Chat.BusinessLogic.Exceptions.BadRequestException
{
    internal class UpdateMessageFromAnotherChatRoomException
        : BadRequestException
    {
        public UpdateMessageFromAnotherChatRoomException(Guid issuerId) 
            : base($"User with {issuerId} tried to update message within another chat room")
        {
        }
    }
}
