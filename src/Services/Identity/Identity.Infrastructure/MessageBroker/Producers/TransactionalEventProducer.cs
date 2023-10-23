using Confluent.Kafka;
using Identity.Application.Interfaces.MessageBroker.Producer;
using Identity.Infrastructure.MessageBroker.TranscationalOutbox;
using Newtonsoft.Json;
using Shared.Enums;
using Shared.Messages;

namespace Identity.Infrastructure.MessageBroker.Producers
{
    public class TransactionalEventProducer : ITransactionalEventProducer
    {
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public TransactionalEventProducer(IOutboxMessageRepository outboxMessageRepository)
        {
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task ProduceAsync<Tv>(string topic, string key, Tv value)
            where Tv : class
        {
            var payload = JsonConvert.SerializeObject(value);

            var outboxMessage = new OutboxMessage()
            {
                Id = new Guid(key),
                MessageType = value.GetType().ToString(),
                Status = OutboxMessageStatus.New,
                Payload = payload,
                Topic = topic
            };

            await _outboxMessageRepository.CreateAsync(outboxMessage);
        }
    }
}
