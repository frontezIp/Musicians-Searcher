namespace Musicians.Domain.Exceptions
{
    public class NotAuthenticatedException : Exception
    {
        public NotAuthenticatedException() 
            : base("Your are not authenticated")
        {
        }
    }
}
