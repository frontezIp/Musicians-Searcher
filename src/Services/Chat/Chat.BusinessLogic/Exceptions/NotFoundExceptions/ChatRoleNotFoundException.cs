namespace Chat.BusinessLogic.Exceptions.NotFoundExceptions
{
    internal class ChatRoleNotFoundException : NotFoundException
    {
        public ChatRoleNotFoundException() 
            : base("Such chat role does not exists")
        {
        }
    }
}
