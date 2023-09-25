namespace Chat.BusinessLogic.DTOs.ResponseDTOs
{
    public class ChatRoomResponseDto
    {
        public Guid Id { get; set; }    
        public string Title { get; set; } = string.Empty;
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
        public int MembersNumber { get; set; }
        public int MessagesCount { get; set; }
        public List<MessageResponseDto> Messages { get; set; } = new();
    }
}
