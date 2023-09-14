namespace Chat.BusinessLogic.Exceptions.BadRequestException
{
    public class IdParameterBadRequestException
        : BadRequestException
    {
        public IdParameterBadRequestException(string collectionName) 
            : base($"Ids parameter of the given {collectionName} collection is null")
        {
        }
    }
}
