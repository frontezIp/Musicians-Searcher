using Musicians.Domain.RequestFeatures;
using Musicians.Domain.RequestParameters;
using Musicians.Infrastructure.Models;

namespace Musicians.Application.Interfaces.Persistance
{
    public interface IMusiciansRepository : IBaseRepository<Musician>
    {
        Task<PagedList<Musician>> GetFilteredMusiciansAsync(MusicianParameters parameters, CancellationToken cancellationToken = default);
        Task UpdateMusicianProfileAsync(Musician musicianToUpdate);
    }
}
