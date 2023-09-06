using MediatR;
using Musicians.Application.DTOs.RequestDTOs;
using Musicians.Application.DTOs.ResponseDTOs;
using Musicians.Domain.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Musicians.Application.MediatoR.Features.Musicians.Queries.GetFilteredMusicians
{
    public record GetFilteredMusiciansQuery(GetFilteredMusiciansRequestDto GetFilteredMusiciansRequestDto)
        : IRequest<(IEnumerable<MusicianResponseDto> musicians, MetaData metaData)>;
}
