namespace Chat.BusinessLogic.DTOs.ResponseDTOs
{
    public class MessageResponseDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public MessengerUserResponseDto MessengerUser { get; set; } = null!;
    }
}
