using MediatR;

namespace Musicians.Application.MediatoR.Features.Friends.Commands.AddFriend
{
    public record AddFriendToMusicianCommand(Guid musicianId, Guid musicianToAddId):
        IRequest;
}
