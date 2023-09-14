namespace Musicians.Domain.Exceptions.BadRequestException
{
    public class CollectionByIdsBadRequestException : BadRequestException
    {
        public CollectionByIdsBadRequestException(string collectionName) 
            : base($"Some of the given ids is not exists in the {collectionName} collection")
        { }
    }
}
