using FluentValidation;
using Musicians.Application.Validators.RequestValidators;

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
