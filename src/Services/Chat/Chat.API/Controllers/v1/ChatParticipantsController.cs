using Chat.BusinessLogic.DTOs.RequestDTOs.ChatParticipantRequestsDTOs;
using Chat.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers.v1
{
    [Route("api/{v:apiversion}/MessengerUsers/{messengerUserId}/ChatRooms/{chatRoomId}/[controller]")]
    public class ChatParticipantsController : BaseApiController
    {
        private readonly IChatParticipantService _chatParticipantService;

        public ChatParticipantsController(IChatParticipantService chatParticipantService)
        {
            _chatParticipantService = chatParticipantService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetChatParticipantsAsync(Guid messengerUserId,
            Guid chatRoomId,
            [FromQuery]GetFilteredChatParticipantsRequestDto getFilteredChatParticipantsRequestDto)
        {
            var result = await _chatParticipantService.GetChatParticipantsAsync(messengerUserId,
                chatRoomId,
                getFilteredChatParticipantsRequestDto,
                HttpContext.RequestAborted);

            AddPaginationInfo(result.metaData);

            return Ok(result.chatParticipants);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> AddChatParticipantsAsync(Guid messengerUserId,
            Guid chatRoomId,
            [FromBody]List<Guid> messengerUsersIdsToAdd)
        {
            await _chatParticipantService.AddChatParticipantsAsync(messengerUserId,
                chatRoomId,
                messengerUsersIdsToAdd,
                HttpContext.RequestAborted);

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteChatParticipantsAsync(Guid messengerUserId,
            Guid chatRoomId,
            [FromBody]List<Guid> messengerUsersIdsToDelete)
        {
            await _chatParticipantService.DeleteChatParticipantsAsync(messengerUserId,
                chatRoomId,
                messengerUsersIdsToDelete,
                HttpContext.RequestAborted);

            return NoContent();
        }

        [HttpPut("{chatParticipantToUpdateId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateChatParticipantRole(Guid messengerUserId,
            Guid chatRoomId,
            Guid chatParticipantToUpdateId,
            Guid newRoleId)
        {
            await _chatParticipantService.UpdateChatParticipantRoleAsync(messengerUserId,
                chatRoomId,
                chatParticipantToUpdateId,
                newRoleId,
                HttpContext.RequestAborted);

            return NoContent();
        }
    }
}
