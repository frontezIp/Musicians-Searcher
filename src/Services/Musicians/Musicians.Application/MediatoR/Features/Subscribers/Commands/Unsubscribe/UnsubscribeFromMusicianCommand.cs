using MediatR;

namespace Musicians.Application.MediatoR.Features.Subscribers.Commands.Unsubscribe
{
    public record UnsubscribeFromMusicianCommand(Guid musicianId, Guid musicianToUnsubscribeId) 
        : IRequest;
}
