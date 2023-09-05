using Identity.Application.DTOs.ResponseDTOs;

namespace Identity.Application.Interfaces.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryResponseDto>> GetAllLocationsAsync();
    }
}
