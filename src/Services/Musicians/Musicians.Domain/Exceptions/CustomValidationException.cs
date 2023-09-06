namespace Musicians.Domain.Exceptions
{
    public class CustomValidationException : Exception
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; set; }

        public CustomValidationException(IReadOnlyDictionary<string, string[]> errors)
            : base("One or more validation errors occured")
            => Errors = errors;
    }
}
