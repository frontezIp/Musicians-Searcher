using Chat.BusinessLogic.DTOs.RequestDTOs;
using FluentValidation;

namespace Chat.BusinessLogic.Validators.RequestValidators
{
    public class PaginatedUserChatRoomsRequestDtoValidator : AbstractValidator<PaginatedUserChatRoomsRequestDto>
    {
        public PaginatedUserChatRoomsRequestDtoValidator()
        {
            Include(new RequestParametersDtoValidator());

            RuleFor(request => request.SearchTerm)
                .MaximumLength(60)
                .WithMessage("Search term must not be more than 60 symbols");
        }
    }
}
