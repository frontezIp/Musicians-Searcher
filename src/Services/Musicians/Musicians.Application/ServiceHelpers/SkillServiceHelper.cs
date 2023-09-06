using Musicians.Application.Interfaces.Persistance;
using Musicians.Application.Interfaces.ServiceHelpers;
using Musicians.Domain.Exceptions.BadRequestException;
using Musicians.Domain.Models;

namespace Musicians.Application.ServiceHelpers
{
    internal class SkillServiceHelper : ISkillServiceHelper
    {
        private readonly ISkillsRepository _skillsRepository;

        public SkillServiceHelper(ISkillsRepository skillsRepository)
        {
            _skillsRepository = skillsRepository;
        }

        public async Task<IEnumerable<Skill>> CheckIfSkillsByGivenIdsExistsAndGet(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            if (ids is null)
                throw new IdsParameterBadRequestException(nameof(Skill));

            var skills = await _skillsRepository.GetByIdsAsync(ids, cancellationToken);

            if (skills.Count() != ids.Count())
                throw new CollectionByIdsBadRequestException(nameof(Skill));

            return skills;
        }

        public async Task CheckSkillsByGivenIdsExists(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            await CheckIfSkillsByGivenIdsExistsAndGet(ids, cancellationToken);
        }
    }
}
