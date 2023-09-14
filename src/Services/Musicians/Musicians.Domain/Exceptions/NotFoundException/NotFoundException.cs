namespace Musicians.Domain.Exceptions.NotFoundException
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
