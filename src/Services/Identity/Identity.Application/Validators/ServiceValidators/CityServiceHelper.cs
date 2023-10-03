using Identity.Application.Interfaces.Persistance;
using Identity.Application.Interfaces.ServiceHelpers;
using Identity.Domain.Exceptions.NotFoundException;
using Identity.Domain.Models;

namespace Identity.Application.Validators.ServiceHelpers
{
    internal class CityServiceHelper : ICityServiceHelper
    {
        private readonly ICityRepository _cityRepository;

        public CityServiceHelper(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<City> CheckIfCityExistsAndGetAsync(Guid cityId, bool trackChanges)
        {
            var city = await _cityRepository.GetCityByIdWithIncludedCountry(cityId, trackChanges);
            if (city == null)
                throw new CityNotFoundException();

            return city;
        }
    }
}
