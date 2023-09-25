using Chat.DataAccess.Models;

namespace Chat.BusinessLogic.DTOs.ResponseDTOs
{
    public class MessengerUserResponseDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Photo { get; set; }
    }
}
