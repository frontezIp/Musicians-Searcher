using FluentValidation;
using MediatR;
using Musicians.Domain.Exceptions;

namespace Musicians.Application.MediatoR.Behaviours
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var errorsDictionary = _validators
                .Select(validator => validator.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(result => result != null)
                .GroupBy(
                    key => key.PropertyName.Substring(key.PropertyName.IndexOf('.') + 1),
                    error => error.ErrorMessage,
                    (key, errorMessages) => new
                    {
                        property = key,
                        ErrorMessages = errorMessages.Distinct().ToArray()
                    })
                .ToDictionary(key => key.property, errors => errors.ErrorMessages);

            if (errorsDictionary.Any())
                throw new CustomValidationException(errorsDictionary);

            return await next();
        }
    }
}
