using Identity.Domain.Models;

namespace Identity.Application.Interfaces.Persistance
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetAllCountriesAsync(bool trackChanges);
        void CreateCountry(Country country);
        void DeleteCountry(Country country);
        Task<IEnumerable<Country>> GetAllCountriesWithIncludedCitiesAsync(bool trackChanges);
    }
}
