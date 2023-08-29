namespace Chat.DataAccess.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ChatRoom ChatRoom { get; set; } = null!;
        public Guid ChatRoomId { get; set; }
        public MessangerUser MessangerUser { get; set; } = null!;
        public Guid MessangerUserId { get; set; }
    }
}
