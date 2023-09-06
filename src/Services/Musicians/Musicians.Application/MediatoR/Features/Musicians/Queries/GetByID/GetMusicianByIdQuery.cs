using MediatR;
using Musicians.Application.DTOs.ResponseDTOs;

namespace Musicians.Application.MediatoR.Features.Musicians.Queries.GetByID
{
    public record GetMusicianByIdQuery(Guid musicianId)
        : IRequest<MusicianResponseDto>;
}
