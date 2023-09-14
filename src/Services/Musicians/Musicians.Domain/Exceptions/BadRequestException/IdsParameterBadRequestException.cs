namespace Musicians.Domain.Exceptions.BadRequestException
{
    public class IdsParameterBadRequestException : BadRequestException
    {
        public IdsParameterBadRequestException(string collectionName) 
            : base($"Ids parameter of the given {collectionName} collection is null")
        { }
    }
}
