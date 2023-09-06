using FluentValidation;

namespace Musicians.Application.MediatoR.Features.Friends.Commands.AddFriend
{
    internal class AddFriendToMusicianCommandValidator : AbstractValidator<AddFriendToMusicianCommand>
    {
        public AddFriendToMusicianCommandValidator()
        {
            RuleFor(command => command.musicianId)
                .NotEmpty()
                .WithMessage("MusicianId must not be empty");

            RuleFor(command => command.musicianToAddId)
                .NotEmpty()
                .WithMessage("Musician to add in friends id must not be empty");
        }
    }
}
