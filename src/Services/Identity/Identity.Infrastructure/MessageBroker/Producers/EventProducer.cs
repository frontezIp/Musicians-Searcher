using Confluent.Kafka;
using Identity.Application.Interfaces.MessageBroker.Producer;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Identity.Infrastructure.MessageBroker.Producers
{
    internal class EventProducer : IEventProducer
    {
        private readonly IOptions<ProducerConfig> _options;

        public EventProducer(IOptions<ProducerConfig> options)
        {
            _options = options;
        }

        public async Task ProduceAsync<Tk,Kv>(string topic, Tk key, Kv value) where Kv : class
        {
            var config = _options.Value;
            using var producer = new ProducerBuilder<Tk, Kv>(config)
                .SetValueSerializer(new KafkaSerializer<Kv>())
                .Build();

            var eventMessage = new Message<Tk, Kv>
            {
                Key = key,
                Value = value
            };

            var deliveryResult = await producer.ProduceAsync(topic, eventMessage);

            if (deliveryResult.Status == PersistenceStatus.NotPersisted)
            {
                throw new Exception($"Could not produce message with {key} key value to topic - {topic} due to the following reason: {deliveryResult.Message}.");       
            }
        }
    }
}
