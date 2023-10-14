namespace Musicians.Domain.Exceptions.BadRequestException
{
    public class InvalidFormatBadRequestException<T> : BadRequestException
    {
        public InvalidFormatBadRequestException(string actualValue) 
            : base($"Value - {actualValue} is in the incorrect format with {typeof(T).Name} type was send by the user")
        {
        }
    }
}
