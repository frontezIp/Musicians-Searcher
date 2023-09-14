namespace Chat.BusinessLogic.Exceptions.BadRequestException
{
    public class DeleteChatParticipantWithHigherOrEqualChatRoleBadRequestException
        : BadRequestException
    {
        public DeleteChatParticipantWithHigherOrEqualChatRoleBadRequestException(Guid issuerId)
            : base($"Chat participant with {issuerId} failed to delete one of the chat participants" +
                  $" due to equal or lower permissions")
        {
        }
    }
}
