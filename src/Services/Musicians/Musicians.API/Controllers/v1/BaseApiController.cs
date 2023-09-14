using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Musicians.API.Controllers.v1
{
    [Authorize]
    [Route("api/{v:apiversion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class BaseApiController : ControllerBase
    {
    }
}
