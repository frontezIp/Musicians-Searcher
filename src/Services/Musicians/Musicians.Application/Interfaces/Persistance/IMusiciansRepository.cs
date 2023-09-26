using Musicians.Domain.Models;
using Musicians.Domain.RequestFeatures;
using Musicians.Domain.RequestParameters;

namespace Musicians.Application.Interfaces.Persistance
{
    public interface IMusiciansRepository : IBaseRepository<Musician>
    {
        Task<PagedList<Musician>> GetFilteredMusiciansAsync(MusicianParameters parameters, CancellationToken cancellationToken = default);
        Task UpdateMusicianProfileAsync(Musician musicianToUpdate);
    }
}
