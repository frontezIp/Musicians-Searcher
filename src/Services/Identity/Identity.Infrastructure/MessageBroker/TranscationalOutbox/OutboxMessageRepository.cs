using Identity.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using Shared.Messages;

namespace Identity.Infrastructure.MessageBroker.TranscationalOutbox
{
    internal class OutboxMessageRepository : IOutboxMessageRepository
    {
        private readonly IdentityContext _context;

        public OutboxMessageRepository(IdentityContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(OutboxMessage message)
        {
            await _context.AddAsync(message);
        }

        public void Update(OutboxMessage message)
        {
            _context.Update(message);
        }

        public async Task<IEnumerable<OutboxMessage>> GetOutboxMessagesToSentAsync(CancellationToken cancellationToken = default)
        {
            return await _context.OutboxMessages
                .Where(outboxMessage => outboxMessage.Status == Shared.Enums.OutboxMessageStatus.New)
                .ToListAsync(cancellationToken);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
