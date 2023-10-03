using Shared.Messages;

namespace Identity.Infrastructure.MessageBroker.TranscationalOutbox
{
    public interface IOutboxMessageRepository
    {
        Task CreateAsync(OutboxMessage message);
        Task<IEnumerable<OutboxMessage>> GetOutboxMessagesToSentAsync(CancellationToken cancellationToken = default);
        Task SaveChangesAsync();
        void Update(OutboxMessage message);
    }
}
