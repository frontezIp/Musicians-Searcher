namespace Identity.Domain.Exceptions.NotFoundException
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException() 
            : base("Such user doesn't exists")
        {
        }
    }
}
