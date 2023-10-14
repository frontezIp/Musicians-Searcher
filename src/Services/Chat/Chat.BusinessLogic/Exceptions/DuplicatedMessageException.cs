namespace Chat.BusinessLogic.Exceptions
{
    public class DuplicatedMessageException : Exception
    {
        public DuplicatedMessageException(string messageType, string messageId) 
            : base($"Duplicated {messageType} message with {messageId} was found")
        {
        }
    }
}
