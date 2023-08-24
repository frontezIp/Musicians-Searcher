using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DataAccess.Models
{
    public class MessangerUser
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string? Photo { get; set; }
        public List<Message> Messages { get; set; } = new();
        public List<ChatParticipant> ParticipatedChats { get; set; } = new();   
        public List<ChatRoom> CreatedChatRooms { get; set; } = new();  
    }
}
