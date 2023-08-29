namespace Identity.Domain.Exceptions.BadRequestException
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        { }
    }
}
