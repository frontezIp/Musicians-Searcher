using Musicians.Domain.Models;

namespace Musicians.Application.Interfaces.ServiceHelpers
{
    public interface IMusicianServiceHelper
    {
        Task<Musician> CheckIfMusicianExistsAndGetAsync(Guid musicianId, CancellationToken cancellationToken = default);
        Task CheckIfMusicianExistsAsync(Guid musicianId, CancellationToken cancellationToken = default);
    }
}
