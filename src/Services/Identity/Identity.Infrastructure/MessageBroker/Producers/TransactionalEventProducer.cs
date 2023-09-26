using Confluent.Kafka;
using Identity.Application.Interfaces.MessageBroker.Producer;
using Identity.Infrastructure.MessageBroker.TranscationalOutbox;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Shared.Enums;
using Shared.Messages;

namespace Identity.Infrastructure.MessageBroker.Producers
{
    public class TransactionalEventProducer : ITransactionalEventProducer
    {
        private readonly IOptions<ProducerConfig> _options;
        private readonly IOutboxMessageRepository _outboxMessageRepository;

        public TransactionalEventProducer(IOptions<ProducerConfig> options,
            IOutboxMessageRepository outboxMessageRepository)
        {
            _options = options;
            _outboxMessageRepository = outboxMessageRepository;
        }

        public async Task ProduceAsync<Tv>(string topic, string key, Tv value)
            where Tv : class
        {
            //var config = _options.Value;
            //using var producer = new ProducerBuilder<string, OutboxMessage>(config)
            //    .SetValueSerializer(new KafkaSerializer<OutboxMessage>())
            //    .Build();

            var payload = JsonConvert.SerializeObject(value);

            var outboxMessage = new OutboxMessage()
            {
                Id = new Guid(key),
                MessageType = value.GetType().ToString(),
                Status = OutboxMessageStatus.New,
                Payload = payload,
                Topic = topic
            };

            //var eventMessage = new Message<string, OutboxMessage>
            //{
            //    Key = key,
            //    Value = outboxMessage
            //};

            await _outboxMessageRepository.Create(outboxMessage);
            //try
            //{
            //    var deliveryResult = await producer.ProduceAsync(topic, eventMessage);

            //    if (deliveryResult.Status == PersistenceStatus.NotPersisted)
            //    {
            //        outboxMessage.Status = OutboxMessageStatus.New;
            //        _outboxMessageRepository.Update(outboxMessage);
            //        throw new Exception($"Could not produce message with {key} key value to topic - {topic} due to the following reason: {deliveryResult.Message}.");
            //    }
            //} catch (Exception)
            //{
            //    outboxMessage.Status = OutboxMessageStatus.New;
            //    _outboxMessageRepository.Update(outboxMessage);
            //    throw;
            //}
        }
    }
}
