namespace Chat.BusinessLogic.Exceptions.AlreadyExistsException
{
    public class MessengerUsersAlreadyExistsAsChatParticipantsException : AlreadyExistsException
    {
        public MessengerUsersAlreadyExistsAsChatParticipantsException(string message)
            : base(message)
        {
        }
    }
}
