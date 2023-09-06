namespace Musicians.Domain.Exceptions.AlreadyExistsException
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string message) : base(message)
        {
        }
    }
}
