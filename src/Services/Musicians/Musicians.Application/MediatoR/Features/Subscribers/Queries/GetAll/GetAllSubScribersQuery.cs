using MediatR;
using Musicians.Application.DTOs.ResponseDTOs;

namespace Musicians.Application.MediatoR.Features.Subscribers.Queries.GetAll
{
    public record GetAllSubScribersQuery(Guid musicianId) :
        IRequest<IEnumerable<MusicianResponseDto>>;
}
