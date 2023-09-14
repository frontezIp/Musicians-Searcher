using Chat.BusinessLogic.DTOs.RequestDTOs.ChatParticipantRequestsDTOs;
using FluentValidation;

namespace Chat.BusinessLogic.Validators.RequestValidators.ChatParticipantsValidators
{
    public class GetFilteredChatParticipantsRequestDtoValidator : AbstractValidator<GetFilteredChatParticipantsRequestDto>
    {
        public GetFilteredChatParticipantsRequestDtoValidator()
        {
        }
    }
}
