using Identity.Application.Interfaces.Persistance;
using Identity.Domain.Models;
using Identity.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Persistance.Repositories
{
    internal class CountryRepository : RepositoryBase<Country>, ICountryRepository
    {
        public CountryRepository(IdentityContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Country>> GetAllCountriesWithIncludedCitiesAsync(bool trackChanges)
        {
            return await GetAll(trackChanges)
                .Include(c => c.Cities)
                .ToListAsync();
        }
    }
}
