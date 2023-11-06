using Chat.BusinessLogic.DTOs.ResponseDTOs;

namespace Chat.BusinessLogic.SignalR.ClientInterfaces
{
    public interface IChatClient
    {
        Task<ChatRoomResponseDto> JoinChatRoomAsync(Guid chatRoomId);
        Task SendMessage(MessageResponseDto messageResponseDto);
    }
}
