namespace Chat.DataAccess.Models
{
    public class ChatParticipant
    {
        public Guid Id { get; set; }
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
        public MessangerUser MessangerUser { get; set; } = null!;
        public Guid MessangerUserId { get; set; }  
        public ChatRole ChatRole { get; set; } = null!;
        public Guid ChatRoleId { get; set; }
        public ChatRoom ChatRoom { get; set; } = null!;
        public Guid ChatRoomId { get; set;}

    }
}
