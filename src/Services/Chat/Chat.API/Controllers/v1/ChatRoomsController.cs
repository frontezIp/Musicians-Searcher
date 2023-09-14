using Chat.BusinessLogic.DTOs.RequestDTOs;
using Chat.BusinessLogic.DTOs.RequestDTOs.ChatRoomRequestsDTOs;
using Chat.BusinessLogic.DTOs.RequestDTOs.MessageRequestsDTOs;
using Chat.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chat.API.Controllers.v1
{
    [Route("api/{v:apiversion}/MessengerUsers/{messengerUserId}/[controller]")]
    public class ChatRoomsController : BaseApiController
    {
        private readonly IChatRoomService _chatRoomsService;

        public ChatRoomsController(IChatRoomService chatRoomsService)
        {
            _chatRoomsService = chatRoomsService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetChatRoomsAsync(Guid messengerUserId,
            [FromQuery]PaginatedUserChatRoomsRequestDto paginatedUserChatRoomsRequestDto)
        {
            var result = await _chatRoomsService.GetChatRoomsOfGivenMessengerUserAsync(messengerUserId,
                paginatedUserChatRoomsRequestDto,
                HttpContext.RequestAborted);

            AddPaginationInfo(result.metaData);

            return Ok(result.chatRooms);
        }

        [HttpGet("{chatRoomId}", Name = "ChatRoomById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetChatRoomAsync(Guid messengerUserId, Guid chatRoomId,
            [FromQuery]GetMessagesRequestDto getMessagesRequestDto)
        {
            var result = await _chatRoomsService.GetChatRoomAsync(messengerUserId, chatRoomId,
                getMessagesRequestDto,
                HttpContext.RequestAborted);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateChatRoomAsync(Guid messengerUserId, 
            [FromBody]CreateChatRoomRequestDto createChatRoomRequestDto) 
        {
            var result = await _chatRoomsService.CreateChatRoomAsync(messengerUserId,
                createChatRoomRequestDto,
                HttpContext.RequestAborted);

            var getChatRoomRequest = new GetMessagesRequestDto();

            return CreatedAtRoute("ChatRoomById", new { messengerUserId = messengerUserId, chatRoomId = result.Id, getMessageRequestDto = getChatRoomRequest }, result);
        }

        [HttpPut("chatRoomId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateChatRoomAsync(Guid messengerUserId, 
            Guid chatRoomId,
            [FromBody]UpdateChatRoomRequestDto updateChatRoomRequestDto)
        {
            await _chatRoomsService.UpdateChatRoomAsync(messengerUserId,
                chatRoomId,
                updateChatRoomRequestDto,
                HttpContext.RequestAborted);

            return NoContent();
        }
    }
}
