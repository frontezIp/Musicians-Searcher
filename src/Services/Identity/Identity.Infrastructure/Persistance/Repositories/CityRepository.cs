using Identity.Application.Interfaces.Persistance;
using Identity.Domain.Models;
using Identity.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistance.Repositories
{
    internal class CityRepository : RepositoryBase<City>, ICityRepository
    {
        public CityRepository(IdentityContext context) : base(context)
        {
        }

        public void CreateCity(City city)
        {
            Create(city);
        }

        public void DeleteCity(City city)
        {
            Delete(city);
        }

        public async Task<List<City>> GetAllCitiesAsync(bool trackChanges)
        {
            return await GetAllAsync(trackChanges).ToListAsync();
        }

        public async Task<City?> GetCityById(Guid id,  bool trackChanges)
        {
            return await GetByConditionAsync(city => city.Id == id, trackChanges)
                .Include(c => c.Country)
                .SingleOrDefaultAsync();
        }
    }
}
