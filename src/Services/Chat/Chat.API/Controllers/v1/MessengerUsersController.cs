using Chat.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers.v1
{
    public class MessengerUsersController : BaseApiController
    {
        private readonly IMessengerUserService _messengerUserService;

        public MessengerUsersController(IMessengerUserService messengerUserService)
        {
            _messengerUserService = messengerUserService;
        }

        [HttpGet("{messengerUserId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMessengerUserProfileAsync(Guid messengerUserId)
        {
            var messengerToReturn = await _messengerUserService.GetMessengerUserProfileInfoAsync(messengerUserId, HttpContext.RequestAborted);

            return Ok(messengerToReturn);
        }

        [HttpGet("{messengerUserId}/friends")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMessengerUserFriendsAsync(Guid messengerUserId)
        {
            var messengerUserFriends = await _messengerUserService.GetMessengerUserFriendsAsync(messengerUserId, HttpContext.RequestAborted);

            return Ok(messengerUserFriends);
        }
    }
}
