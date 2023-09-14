using Chat.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers.v1
{
    public class ChatRolesController : BaseApiController
    {
        private readonly IChatRoleService _chatRoleService;

        public ChatRolesController(IChatRoleService chatRoleService)
        {
            _chatRoleService = chatRoleService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var result = await _chatRoleService.GetAllChatRolesAsync(HttpContext.RequestAborted);

            return Ok(result);
        }
    }
}
