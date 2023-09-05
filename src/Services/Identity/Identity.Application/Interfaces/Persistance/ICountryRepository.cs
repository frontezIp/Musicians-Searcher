using Identity.Domain.Models;

namespace Identity.Application.Interfaces.Persistance
{
    public interface ICountryRepository : IRepositoryBase<Country>
    {
        Task<IEnumerable<Country>> GetAllCountriesWithIncludedCitiesAsync(bool trackChanges);
    }
}
