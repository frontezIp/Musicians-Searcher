using MediatR;
using Musicians.Application.DTOs.ResponseDTOs;

namespace Musicians.Application.MediatoR.Features.Skills.Queries.GetAll
{
    public record GetAllSkillsQuery() : 
        IRequest<IEnumerable<SkillResponseDto>>;
}
