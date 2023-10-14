using Chat.BusinessLogic.DTOs.RequestDTOs.MessageRequestsDTOs;
using FluentValidation;

namespace Chat.BusinessLogic.Validators.RequestValidators.MessageValidators
{
    public class CreateMessageRequestDtoValidator : AbstractValidator<CreateMessageRequestDto>
    {
        public CreateMessageRequestDtoValidator()
        {
            RuleFor(message => message.Text)
                .NotNull()
                .NotEmpty()
                .WithMessage("Message must not be empty")
                .MaximumLength(400)
                .WithMessage("Message must be 400 symbols or fewer");

            When(message => message.Delay != null, () =>
            {
                RuleFor(message => message.Delay!.Value)
                    .NotEmpty()
                    .GreaterThan(DateTime.Now.AddMinutes(1))
                    .WithMessage("Delay must be at least one minute ahead over current time");
            });
        }
    }
}
