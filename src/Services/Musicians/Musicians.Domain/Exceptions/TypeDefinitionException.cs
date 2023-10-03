namespace Musicians.Domain.Exceptions
{
    public class TypeDefinitionException : Exception
    {
        public TypeDefinitionException(string notDefinedType) 
            : base($"The given type {notDefinedType} could not be determined")
        {
        }
    }
}
