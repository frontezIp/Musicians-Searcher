using Shared.Enums;

namespace Shared.Messages
{
    public class OutboxMessage
    {
        public Guid Id { get; set; }
        public string Payload { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;
        public string MessageType { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public OutboxMessageStatus Status { get; set; } = OutboxMessageStatus.New;
    }
}
