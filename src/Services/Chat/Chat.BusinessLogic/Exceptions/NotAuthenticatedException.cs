namespace Chat.BusinessLogic.Exceptions
{
    public class NotAuthenticatedException : Exception
    {
        public NotAuthenticatedException() 
            :base("You are not authenticated")
        {

        }
    }
}
