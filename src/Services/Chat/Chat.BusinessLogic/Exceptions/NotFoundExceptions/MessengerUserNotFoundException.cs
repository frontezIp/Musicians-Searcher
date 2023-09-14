namespace Chat.BusinessLogic.Exceptions.NotFoundExceptions
{
    public class MessengerUserNotFoundException
        : NotFoundException
    {
        public MessengerUserNotFoundException(Guid messengerUserId) 
            : base($"Messenger user with {messengerUserId} does not exists")
        {
        }
    }
}
