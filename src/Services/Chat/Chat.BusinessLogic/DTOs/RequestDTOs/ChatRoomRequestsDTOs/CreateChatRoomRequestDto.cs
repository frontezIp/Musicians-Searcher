using Chat.DataAccess.Models;

namespace Chat.BusinessLogic.DTOs.RequestDTOs.ChatRoomRequestsDTOs
{
    public class CreateChatRoomRequestDto
    {
        public string Title { get; set; } = string.Empty;
        public List<Guid> MessengerUsersIdsToAdd { get; set; } = new();
    }
}
