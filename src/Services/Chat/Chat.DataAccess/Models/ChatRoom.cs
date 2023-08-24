using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DataAccess.Models
{
    public class ChatRoom
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateOnly CreatedAt { get; set; }
        public DateOnly UpdatedAt { get; set; }
        public MessangerUser Creater { get; set; } = null!;
        public Guid CreaterId { get; set; }
        public List<ChatParticipant> ChatParticipants { get; set; } = new();
        public List<Message> Messages { get; set; } = new();    
    }
}
