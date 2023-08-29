using Identity.Application.Interfaces.Persistance;
using Identity.Application.Interfaces.ServiceValidators;
using Identity.Domain.Exceptions.NotFoundException;
using Identity.Domain.Models;

namespace Identity.Application.Validators.ServiceValidators
{
    internal class CityServiceValidator : ICityServiceValidator
    {
        private readonly ICityRepository _cityRepository;

        public CityServiceValidator(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<City> CheckIfCityExistsAndGetAsync(Guid cityId, bool trackChanges)
        {
            var city = await _cityRepository.GetCityById(cityId, trackChanges);
            if (city == null)
                throw new CityNotFoundException();
            return city;
        }
    }
}
