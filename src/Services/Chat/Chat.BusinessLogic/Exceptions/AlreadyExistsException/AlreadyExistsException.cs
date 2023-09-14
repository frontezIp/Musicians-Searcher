namespace Chat.BusinessLogic.Exceptions.AlreadyExistsException
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string? message) 
            : base(message)
        {
        }
    }
}
