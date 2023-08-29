using Identity.Application.DTOs.ResponseDTOs;

namespace Identity.Application.Interfaces.Services
{
    public interface ICityService
    {
        public Task<CityResponseDto> GetAllCitiesAsync();
    }
}
