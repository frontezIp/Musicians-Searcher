using Identity.Domain.Models;

namespace Identity.Application.Interfaces.Persistance
{
    public interface ICityRepository
    {
        Task<List<City>> GetAllCitiesAsync(bool trackChanges);
        void CreateCity(City country);
        void DeleteCity(City country);
        Task<City?> GetCityById(Guid id, bool trackChanges);
    }
}
