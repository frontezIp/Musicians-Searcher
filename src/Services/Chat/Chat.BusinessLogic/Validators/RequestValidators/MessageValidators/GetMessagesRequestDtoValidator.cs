using Chat.BusinessLogic.DTOs.RequestDTOs.MessageRequestsDTOs;
using FluentValidation;

namespace Chat.BusinessLogic.Validators.RequestValidators.MessageValidators
{
    public class GetMessagesRequestDtoValidator : AbstractValidator<GetMessagesRequestDto>
    {
        public GetMessagesRequestDtoValidator() 
        {
            Include(new RequestParametersDtoValidator());
        }
    }
}
