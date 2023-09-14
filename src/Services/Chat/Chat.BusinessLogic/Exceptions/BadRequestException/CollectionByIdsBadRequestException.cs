namespace Chat.BusinessLogic.Exceptions.BadRequestException
{
    public class CollectionByIdsBadRequestException
        : BadRequestException
    {
        public CollectionByIdsBadRequestException(string collectionName) 
            : base($"Some of the given ids are not exists in the {collectionName} collection")
        {
        }
    }
}
