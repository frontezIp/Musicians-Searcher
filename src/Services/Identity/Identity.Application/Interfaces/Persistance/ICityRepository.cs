using Identity.Domain.Models;

namespace Identity.Application.Interfaces.Persistance
{
    public interface ICityRepository : IRepositoryBase<City>
    {
        Task<City?> GetCityByIdWithIncludedCountry(Guid id, bool trackChanges);
    }
}
