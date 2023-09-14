namespace Chat.BusinessLogic.Exceptions.NotFoundExceptions
{
    public class MessageNotFoundException
        : NotFoundException
    {
        public MessageNotFoundException(Guid messageId) 
            : base($"Message with {messageId} does not exists")
        {
        }
    }
}
