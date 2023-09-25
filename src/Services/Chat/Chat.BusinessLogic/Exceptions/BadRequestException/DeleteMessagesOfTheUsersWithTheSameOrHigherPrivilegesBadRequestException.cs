namespace Chat.BusinessLogic.Exceptions.BadRequestException
{
    public class DeleteMessagesOfTheUsersWithTheSameOrHigherPrivilegesBadRequestException
        : BadRequestException
    {
        public DeleteMessagesOfTheUsersWithTheSameOrHigherPrivilegesBadRequestException(Guid issuerId)
            : base($"Messenger user with {issuerId} cannot delete messages that belong to the users with the same or higher privileges")
        {
        }
    }
}
