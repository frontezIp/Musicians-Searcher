namespace Chat.DataAccess.Models
{
    public class ChatRoom : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
        public DateTime LastSentMessageAt { get; set; }
        public MessengerUser Creator { get; set; } = null!;
        public Guid CreatorId { get; set; }
        public int MembersNumber { get; set; }  
        public int MessagesCount { get; set; }
        public List<ChatParticipant> ChatParticipants { get; set; } = new();
        public List<Message> Messages { get; set; } = new();    
    }
}
