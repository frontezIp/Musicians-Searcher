namespace Chat.DataAccess.Models
{
    public class Message : BaseEntity
    {
        public string Text { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ChatRoom ChatRoom { get; set; } = null!;
        public Guid ChatRoomId { get; set; }
        public MessengerUser MessengerUser { get; set; } = null!;
        public Guid MessengerUserId { get; set; }
    }
}
