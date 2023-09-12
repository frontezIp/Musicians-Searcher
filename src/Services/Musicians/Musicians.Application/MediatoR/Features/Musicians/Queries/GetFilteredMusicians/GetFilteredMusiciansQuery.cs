using MediatR;
using Musicians.Application.DTOs.RequestDTOs;
using Musicians.Application.DTOs.ResponseDTOs;
using Musicians.Domain.RequestFeatures;

namespace Musicians.Application.MediatoR.Features.Musicians.Queries.GetFilteredMusicians
{
    public record GetFilteredMusiciansQuery(GetFilteredMusiciansRequestDto GetFilteredMusiciansRequestDto)
        : IRequest<(IEnumerable<MusicianResponseDto> musicians, MetaData metaData)>;
}
