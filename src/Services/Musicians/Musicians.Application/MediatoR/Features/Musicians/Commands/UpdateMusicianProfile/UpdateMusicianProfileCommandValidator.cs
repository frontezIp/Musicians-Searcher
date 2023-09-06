using FluentValidation;
using Musicians.Application.Validators.RequestValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicians.Application.MediatoR.Features.Musicians.Commands.UpdateMusicianProfile
{
    internal class UpdateMusicianProfileCommandValidator : AbstractValidator<UpdateMusicianProfileCommand>
    {
        public UpdateMusicianProfileCommandValidator()
        {
            RuleFor(command => command.MusicianProfileRequestDto)
                .SetValidator(new UpdateMusicianProfileRequestDtoValidator());
        }
    }
}
