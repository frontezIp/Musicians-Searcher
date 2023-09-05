using Identity.Application.DTOs.ResponseDTOs;
using Identity.Application.Interfaces.Persistance;
using Identity.Application.Interfaces.Services;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace Identity.Application.Services
{
    public class CountryService : ICountryService
    {
        private readonly IMapper _mapper;
        private readonly ICountryRepository _countryRepository;
        private readonly ILogger<CountryService> _logger;

        public CountryService(IMapper mapper,
            ICountryRepository countryRepository, 
            ILogger<CountryService> logger)
        {
            _mapper = mapper;
            _countryRepository = countryRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<CountryResponseDto>> GetAllLocationsAsync()
        {
            var countries = await _countryRepository.GetAllCountriesWithIncludedCitiesAsync(false);

            var countriesDtos = _mapper.Map<IEnumerable<CountryResponseDto>>(countries);

            _logger.LogInformation("All locations were successfully retrieved");
            
            return countriesDtos;   
        }
    }
}
