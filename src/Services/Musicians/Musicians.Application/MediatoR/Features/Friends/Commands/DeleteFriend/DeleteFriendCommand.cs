using MediatR;

namespace Musicians.Application.MediatoR.Features.Friends.Commands.DeleteFriend
{
    public record DeleteFriendCommand(Guid musicianId, Guid musicianToDeleteId) : 
        IRequest;
}
