using FluentValidation;

namespace Musicians.Application.MediatoR.Features.Friends.Commands.DeleteFriend
{
    public class DeleteFriendCommandValidator : AbstractValidator<DeleteFriendCommand>
    {
        public DeleteFriendCommandValidator()
        {
            RuleFor(command => command.musicianId)
                .NotEmpty()
                .WithMessage("MusicianId must not be empty");

            RuleFor(command => command.musicianToDeleteId)
                .NotEmpty()
                .WithMessage("Musician to delete from friends id must not be empty");
        }
    }
}
