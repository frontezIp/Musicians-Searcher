namespace Chat.BusinessLogic.Exceptions.NotFoundExceptions
{
    internal class ChatParticipantNotFoundException : NotFoundException
    {
        public ChatParticipantNotFoundException(Guid chatParticipantId)
            : base($"Chat paricipant with {chatParticipantId} does not exists")
        {
        }
    }
}
