using Musicians.Domain.Models;

namespace Musicians.Application.Interfaces.ServiceHelpers
{
    public interface ISkillServiceHelper
    {
        Task<IEnumerable<Skill>> CheckIfSkillsByGivenIdsExistsAndGet(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
        Task CheckSkillsByGivenIdsExists(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    }
}
