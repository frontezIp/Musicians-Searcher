namespace Chat.BusinessLogic.Exceptions.BadRequestException
{
    public class UpdateAnotherUserMessageBadRequestException
        : BadRequestException
    {
        public UpdateAnotherUserMessageBadRequestException(Guid issuerId)
            : base($"Messenger user with {issuerId} tried to update someone others message")
        {
        }
    }
}
