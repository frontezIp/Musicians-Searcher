using Confluent.Kafka;
using Identity.Infrastructure.MessageBroker.TranscationalOutbox;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Shared.Enums;
using Shared.Messages;

namespace Identity.Infrastructure.MessageBroker.BackgroudProducers
{
    public class BackgroudOutboxProducer : BackgroundService
    {
        private readonly IOptions<ProducerConfig> _options;
        private readonly ILogger<BackgroudOutboxProducer> _logger;
        private readonly IServiceProvider _serviceProvider;
        private IOutboxMessageRepository _outboxMessageRepository;

        public BackgroudOutboxProducer(IOptions<ProducerConfig> options,
            ILogger<BackgroudOutboxProducer> logger,
            IServiceProvider serviceProvider)
        {
            _options = options;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            _logger.LogInformation("Transactional outbox producer has started");

            using (var scope = _serviceProvider.CreateScope())
            {
                var config = _options.Value;
                using var producer = new ProducerBuilder<string, OutboxMessage>(config)
                    .SetValueSerializer(new KafkaSerializer<OutboxMessage>())
                    .Build();

                _outboxMessageRepository = scope.ServiceProvider.GetRequiredService<IOutboxMessageRepository>();

                while (!stoppingToken.IsCancellationRequested)
                {
                    var notSentedMessages = await _outboxMessageRepository.GetOutboxMessagesToSentAsync(stoppingToken);
                    if (notSentedMessages.Any())
                    {
                        foreach (var message in notSentedMessages)
                        {
                            try
                            {
                                var eventMessage = new Message<string, OutboxMessage>
                                {
                                    Key = message.Id.ToString(),
                                    Value = message
                                };

                                var deliveryResult = await producer.ProduceAsync(message.Topic, eventMessage);

                                if (deliveryResult.Status == PersistenceStatus.NotPersisted)
                                {
                                    message.Status = OutboxMessageStatus.New;
                                    _outboxMessageRepository.Update(message);
                                    await _outboxMessageRepository.SaveChangesAsync();
                                    throw new Exception($"Could not produce message with {message.Id} key value to topic - {message.Topic} due to the following reason: {deliveryResult.Message}.");
                                }
                                _logger.LogInformation("Trying to send a message with {MessageId} id", eventMessage.Key);
                                message.Status = OutboxMessageStatus.Sent;
                                _outboxMessageRepository.Update(message);
                                await _outboxMessageRepository.SaveChangesAsync();
                                _logger.LogInformation("Send message with {MessageId}", message.Id);
                            }
                            catch (Exception)
                            {
                                message.Status = OutboxMessageStatus.New;
                                _outboxMessageRepository.Update(message);
                                await _outboxMessageRepository.SaveChangesAsync();
                                throw;
                            }
                        }
                    }
                }

            }
        }
    }
}
