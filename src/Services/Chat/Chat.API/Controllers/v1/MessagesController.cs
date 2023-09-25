using Chat.BusinessLogic.DTOs.RequestDTOs.MessageRequestsDTOs;
using Chat.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers.v1
{
    [Route("api/{v:apiversion}/messenger-users/{messengerUserId}/chat-rooms/{chatRoomId}/[controller]")]
    public class MessagesController : BaseApiController
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMessageAsync(Guid messengerUserId,
            Guid chatRoomId,
            [FromBody] CreateMessageRequestDto createMessageRequestDto)
        {
            await _messageService.CreateMessageAsync(messengerUserId, chatRoomId,
                createMessageRequestDto,
                HttpContext.RequestAborted);

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteMessagesAsync(Guid messengerUserId,
            Guid chatRoomId,
            List<Guid> messagesIdsToDelete)
        {
            await _messageService.DeleteMessagesAsync(messengerUserId,
                chatRoomId,
                messagesIdsToDelete,
                HttpContext.RequestAborted);

            return NoContent();
        }

        [HttpPut("{messageToUpdateId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateMessageAsync(Guid messengerUserId,
            Guid chatRoomId,
            Guid messageToUpdateId,
            UpdateMessageRequestDto updateMessageRequestDto)
        {
            await _messageService.UpdateMessageAsync(messengerUserId,
                chatRoomId,
                messageToUpdateId,
                updateMessageRequestDto, HttpContext.RequestAborted);

            return NoContent();
        }
    }
}
