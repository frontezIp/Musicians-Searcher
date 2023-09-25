namespace Chat.BusinessLogic.Exceptions.BadRequestException
{
    public class NotEnoughRightsBadRequestException
        : BadRequestException
    {
        public NotEnoughRightsBadRequestException(Guid ChatParticipantId) 
            : base($"Chat participant with {ChatParticipantId} doesn't have such rights to perform this kind of operation")
        {
        }
    }
}
