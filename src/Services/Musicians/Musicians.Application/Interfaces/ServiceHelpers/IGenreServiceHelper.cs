using Musicians.Domain.Models;

namespace Musicians.Application.Interfaces.ServiceHelpers
{
    public interface IGenreServiceHelper
    {
        Task<IEnumerable<Genre>> CheckIfGenresByGivenIdsExistsAndGet(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
        Task CheckIfGinresByGivenIdsExists(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
    }
}
