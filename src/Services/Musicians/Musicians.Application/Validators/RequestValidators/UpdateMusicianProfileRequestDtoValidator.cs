using FluentValidation;
using Musicians.Application.DTOs.RequestDTOs;

namespace Musicians.Application.Validators.RequestValidators
{
    public class UpdateMusicianProfileRequestDtoValidator : AbstractValidator<UpdateMusicianProfileRequestDto>
    {
        public UpdateMusicianProfileRequestDtoValidator() 
        {
            RuleFor(musician => musician.Goal)
                .MaximumLength(150)
                .WithMessage("Goal must be 150 symbols or fewer");

            RuleFor(musician => musician.SkillsIds)
                .Must(skills => skills.Count < 6)
                .WithMessage("The maximum number of skills per profile is 5");

            RuleFor(musician => musician.FavouriteGenresIds)
                .Must(genres => genres.Count <= 8)
                .WithMessage("The maximum number of favourite genres per profile  is 8");
        }
    }
}
