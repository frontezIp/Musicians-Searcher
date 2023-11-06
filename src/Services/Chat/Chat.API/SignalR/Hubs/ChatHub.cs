using Chat.BusinessLogic.DTOs.RequestDTOs.MessageRequestsDTOs;
using Chat.BusinessLogic.DTOs.ResponseDTOs;
using Chat.BusinessLogic.Services.Interfaces;
using Chat.BusinessLogic.SignalR.ClientInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Chat.API.SignalR.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatClient>
    {
        private readonly IChatRoomService _chatRoomService;
        private readonly IMessageService _messageService;
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(IChatRoomService chatRoomService,
            IMessageService messageService,
            ILogger<ChatHub> logger)
        {
            _chatRoomService = chatRoomService;
            _messageService = messageService;
            _logger = logger;
        }

        public async Task<ChatRoomResponseDto> JoinRoom(Guid chatRoomId,
            GetMessagesRequestDto getMessagesRequestDto)
        {
            _logger.LogInformation(Context.UserIdentifier!);

            var chatRoom = await _chatRoomService.GetChatRoomAsync(new Guid(Context.UserIdentifier!),
                chatRoomId,
                getMessagesRequestDto,
                Context.ConnectionAborted);

            await Groups.AddToGroupAsync(Context.ConnectionId, chatRoomId.ToString(), Context.ConnectionAborted);

            return chatRoom;
        }

        public async Task LeaveRoom(Guid chatRoomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomId.ToString(), Context.ConnectionAborted);
        }

        public async Task SendMessage(Guid chatRoomId,
            CreateMessageRequestDto createMessageRequestDto)
        {
            var createdMessageDto = await _messageService.CreateMessageAsync(new Guid(Context.UserIdentifier!),
                chatRoomId,
                createMessageRequestDto,
                Context.ConnectionAborted);

            if (createdMessageDto is null)
            {
                return;
            }

            await Clients.Group(chatRoomId.ToString())
                .SendMessage(createdMessageDto);
        }
    }
}
