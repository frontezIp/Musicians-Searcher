using Identity.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers.v1
{
    [Route("api/{v:apiversion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class LocationsController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public LocationsController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllLocationsAsync()
        {
            var locations = await _countryService.GetAllLocationsAsync();

            return Ok(locations);
        }
    }
}
