using FluentValidation;
using Musicians.Application.DTOs.RequestDTOs;

namespace Musicians.Application.Validators.RequestValidators
{
    internal class GetFilteredMusiciansRequestDtoValidator : AbstractValidator<GetFilteredMusiciansRequestDto>
    {
        public GetFilteredMusiciansRequestDtoValidator()
        {
            Include(new RequestParametersDtoValidator());

            RuleFor(request => request.SearchTerm)
                .MaximumLength(50)
                .WithMessage("Search term must not be more than 50 symbols");

            RuleFor(request => request.MinAge)
                .LessThanOrEqualTo(150)
                .WithMessage("Min age must be less or equal to 150")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Min age must be greater than 0");

            RuleFor(request => request.MaxAge)
                .LessThanOrEqualTo(150)
                .WithMessage("Max age must be less or equal to 150")
                .GreaterThanOrEqualTo(1)
                .WithMessage("Max age must be greater than 0");

            When(musician => musician.MinAge > 1 && musician.MaxAge < 150, () =>
            {
                RuleFor(musician => musician.MinAge)
                    .Must((model, field) => IsMinAgeLowerThanMax(field, model.MaxAge))
                    .WithMessage("Min age must be lower than max age paramater");
            });
        }

        private bool IsMinAgeLowerThanMax(int minAge, int maxAge)
        {
            return minAge <= maxAge;
        }
    }
}
