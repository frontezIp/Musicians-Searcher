using FluentValidation;
using Musicians.Application.DTOs.RequestDTOs;

namespace Musicians.Application.Validators.RequestValidators
{
    public class RequestParametersDtoValidator : AbstractValidator<RequestParametersDto>
    {
        public RequestParametersDtoValidator() 
        {
            RuleFor(request => request.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page size must be greater than 0");
        }
    }
}
