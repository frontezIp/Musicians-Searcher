namespace Chat.BusinessLogic.Exceptions.BadRequestException
{
    public class DeleteMessagesFromMultipleChatRoomsBadRequestException
        : BadRequestException
    {
        public DeleteMessagesFromMultipleChatRoomsBadRequestException()
            : base("You can't delete messages from multiple chat rooms at the same time")

        {
        }
    }
}
