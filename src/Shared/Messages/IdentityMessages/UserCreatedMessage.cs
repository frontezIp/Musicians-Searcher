using Shared.Enums;

namespace Shared.Messages.IdentityMessages
{
    public class UserCreatedMessage
    { 
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get;set; } = string.Empty;
        public string? Photo { get; set;}
        public string Location { get; set; } = string.Empty;
        public int Age { get; set; }
        public string? Biography { get; set; }   
        public DateTime CreatedAt { get; set; }
        public SexTypes SexTypeId { get; set; } 
    }
}
