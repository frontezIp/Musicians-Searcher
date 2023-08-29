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

        public void CreateCountry(Country country)
        {
            Create(country);
        }

        public void DeleteCountry(Country country)
        {
            Delete(country);
        }

        public async Task<List<Country>> GetAllCountriesAsync(bool trackChanges)
        {
            return await GetAllAsync(trackChanges).ToListAsync();
        }
    }
}
