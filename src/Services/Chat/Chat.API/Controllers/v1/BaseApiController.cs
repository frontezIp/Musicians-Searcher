using Chat.BusinessLogic.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Chat.API.Controllers.v1
{
    [Route("api/{v:apiversion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class BaseApiController : ControllerBase
    {
        [NonAction]
        protected void AddPaginationInfo(MetaData metaData)
        {
            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(metaData));
        }
    }
}
