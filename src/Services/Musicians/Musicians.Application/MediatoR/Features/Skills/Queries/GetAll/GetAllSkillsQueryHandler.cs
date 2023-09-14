using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Musicians.Application.DTOs.ResponseDTOs;
using Musicians.Application.Interfaces.Persistance;

namespace Musicians.Application.MediatoR.Features.Skills.Queries.GetAll
{
    internal class GetAllSkillsQueryHandler : IRequestHandler<GetAllSkillsQuery, IEnumerable<SkillResponseDto>>
    {
        private readonly ISkillsRepository _skillRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllSkillsQueryHandler> _logger;

        public GetAllSkillsQueryHandler(IMapper mapper, ISkillsRepository skillRepository, ILogger<GetAllSkillsQueryHandler> logger)
        {
            _skillRepository = skillRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<SkillResponseDto>> Handle(GetAllSkillsQuery request, CancellationToken cancellationToken)
        {
            var skills = await _skillRepository.GetAllAsync(cancellationToken);

            var skillsDtos = _mapper.Map<IEnumerable<SkillResponseDto>>(skills);

            _logger.LogInformation("All skills was successfully retrieved");

            return skillsDtos;
        }
    }
}
