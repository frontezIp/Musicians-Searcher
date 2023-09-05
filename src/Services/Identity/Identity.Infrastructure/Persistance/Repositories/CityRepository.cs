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

        public async Task<City?> GetCityByIdWithIncludedCountry(Guid id,  bool trackChanges)
        {
            return await GetByCondition(city => city.Id == id, trackChanges)
                .Include(c => c.Country)
                .SingleOrDefaultAsync();
        }
    }
}
