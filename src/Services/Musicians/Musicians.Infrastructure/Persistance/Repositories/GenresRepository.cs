using Musicians.Application.Interfaces.Persistance;
using Musicians.Domain.Models;
using Musicians.Infrastructure.Persistance.Contexts;

namespace Musicians.Infrastructure.Persistance.Repositories
{
    internal class GenresRepository :
        BaseRepository<Genre>, IGenresRepository
    {
        public GenresRepository(MusiciansContext musiciansContext) : base(musiciansContext.GetGenres)
        {
        }
    }
}
