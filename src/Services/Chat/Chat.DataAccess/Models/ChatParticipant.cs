namespace Chat.DataAccess.Models
{
    public class ChatParticipant : BaseEntity
    {
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
        public MessengerUser MessengerUser { get; set; } = null!;
        public Guid MessengerUserId { get; set; }  
        public ChatRole ChatRole { get; set; } = null!;
        public Guid ChatRoleId { get; set; }
        public ChatRoom ChatRoom { get; set; } = null!;
        public Guid ChatRoomId { get; set;}
    }
}
