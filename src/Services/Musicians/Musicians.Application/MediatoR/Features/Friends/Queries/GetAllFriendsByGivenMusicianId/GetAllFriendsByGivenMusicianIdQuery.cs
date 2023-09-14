using MediatR;
using Musicians.Application.DTOs.ResponseDTOs;

namespace Musicians.Application.MediatoR.Features.Friends.Queries.GetAllFriendsByGivenMusicianId
{
    public record GetAllFriendsByGivenMusicianIdQuery(Guid musicianId) :
        IRequest<IEnumerable<MusicianResponseDto>>;
}
