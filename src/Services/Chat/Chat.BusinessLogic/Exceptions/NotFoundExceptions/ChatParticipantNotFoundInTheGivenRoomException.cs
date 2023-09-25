namespace Chat.BusinessLogic.Exceptions.NotFoundExceptions
{
    public class ChatParticipantNotFoundInTheGivenRoomException : NotFoundException
    {
        public ChatParticipantNotFoundInTheGivenRoomException(Guid chatRoomId, Guid chatParticipantId)
            : base($"Chat participant with {chatParticipantId} id not exists in the chat room with {chatRoomId} id")
        {
        }
    }
}
