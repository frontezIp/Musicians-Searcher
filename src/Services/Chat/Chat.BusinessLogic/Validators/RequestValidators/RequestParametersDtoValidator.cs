using Chat.BusinessLogic.DTOs.RequestDTOs;
using FluentValidation;

namespace Chat.BusinessLogic.Validators.RequestValidators
{
    public class RequestParametersDtoValidator : AbstractValidator<RequestParamatersDto>
    {
        public RequestParametersDtoValidator()
        {
            RuleFor(request => request.PageSize)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Page size must be greater than 0");
        }
    }
}
