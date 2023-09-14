﻿using Chat.BusinessLogic.DTOs.RequestDTOs.ChatRoomRequestsDTOs;
using FluentValidation;

namespace Chat.BusinessLogic.Validators.RequestValidators.ChatRoomValidators
{
    public class CreateChatRoomRequestDtoValidator : AbstractValidator<CreateChatRoomRequestDto>
    {
        public CreateChatRoomRequestDtoValidator()
        {
            RuleFor(request => request.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("Title cannot be empty")
                .MaximumLength(60)
                .WithMessage("Title must be 60 symbols or fewer");
        }
    }
}
