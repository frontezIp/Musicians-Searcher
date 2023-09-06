using FluentValidation;
using Identity.Application.DTOs.RequestDTOs;

namespace Identity.Application.Validators.RequestValidators
{
    public class UserRequestValidator : AbstractValidator<UserRequestDto>
    {
        public UserRequestValidator() 
        {
            RuleFor(user => user.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage("Name must be 50 letters or fewer");

            RuleFor(user => user.SecondName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Second name is required")
                .MaximumLength(50)
                .WithMessage("Second name must be 50 letters or fewer");

            RuleFor(user => user.Username)
                .NotNull()
                .NotEmpty()
                .WithMessage("Username is required")
                .MinimumLength(6)
                .WithMessage("Username must contain at least 6 symbols")
                .MaximumLength(50)
                .WithMessage("Username must be 50 letters or fewer");

            RuleFor(user => user.Password)
                .NotNull()
                .NotEmpty()
                .WithMessage("Password is required")
                .MaximumLength(50)
                .WithMessage("Password must be 50 symbols or fewer")
                .MinimumLength(6)
                .WithMessage("Password must contain at least 6 symbols");

            RuleFor(user => user.SexTypeId)
                .IsInEnum()
                .WithMessage("Invalid sex type");

            RuleFor(user => user.CityId)
                .NotEmpty()
                .WithMessage("City and country is required");

            RuleFor(user => user.Biography)
                .MaximumLength(350)
                .WithMessage("Biography must be 350 symbols or fewer");

            RuleFor(user => user.Age)
                .NotEmpty()
                .WithMessage("Age is required")
                .GreaterThanOrEqualTo(6)
                .WithMessage("Age must be 6 or greater")
                .LessThanOrEqualTo(140)
                .WithMessage("Age cannot be greater than 140");
                
        }
    }
}
