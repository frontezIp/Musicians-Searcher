namespace Identity.Domain.Exceptions.BadRequestException
{
    public class UserInvalidCredentialsBadRequestException : BadRequestException
    {
        public UserInvalidCredentialsBadRequestException(string message)
         : base(message) { }
    }
}
