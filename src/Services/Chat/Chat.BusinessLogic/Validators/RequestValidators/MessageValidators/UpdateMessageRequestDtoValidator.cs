using Chat.BusinessLogic.DTOs.RequestDTOs.MessageRequestsDTOs;
using FluentValidation;

namespace Chat.BusinessLogic.Validators.RequestValidators.MessageValidators
{
    public class UpdateMessageRequestDtoValidator : AbstractValidator<UpdateMessageRequestDto>
    {
        public UpdateMessageRequestDtoValidator()
        {
            RuleFor(message => message.Text)
                .NotNull()
                .NotEmpty()
                .WithMessage("Message must not be empty")
                .MaximumLength(400)
                .WithMessage("Message must be 400 symbols or fewer");
        }
    }
}
