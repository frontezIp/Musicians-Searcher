using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Musicians.Domain.Exceptions;
using Musicians.Infrastructure.Kafka.ConsumerHandlers;
using Newtonsoft.Json;
using Shared.Messages;

namespace Musicians.Infrastructure.Kafka.BackgroundConsumers
{
    public class OutboxMessagesBackgroundConsumer<Tk> : BackgroundService
    {
        private IOutboxMessageHandler<Tk> _consumerHandler;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<ConsumerConfig> _consumerConfig;
        private readonly ILogger<OutboxMessagesBackgroundConsumer<Tk>> _logger;
        private readonly string _topic;

        public OutboxMessagesBackgroundConsumer(
            string topic,
            IServiceProvider serviceProvider,
            IOptions<ConsumerConfig> consumerConfig,
            ILogger<OutboxMessagesBackgroundConsumer<Tk>> logger)
        {
            _serviceProvider = serviceProvider;
            _consumerConfig = consumerConfig;
            _logger = logger;
            _topic = topic;
        }

        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            _logger.LogInformation("Consumer of the {Outbox} messages has started", nameof(OutboxMessage));

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var builder = new ConsumerBuilder<Tk, OutboxMessage>(_consumerConfig.Value)
                    .SetValueDeserializer(new KafkaDeserializer<OutboxMessage>());

                using var consumer = builder.Build();

                consumer.Subscribe(_topic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = consumer.Consume(stoppingToken);

                    if (result == null)
                    {
                        continue;
                    }

                    var assemblyName = MessagesAssemblyReference.Assembly.GetName().Name;
                    var typeName = $"{result.Message.Value.MessageType}, {assemblyName}";
                    var type = Type.GetType(typeName);

                    if (type == null)
                    {
                        consumer.StoreOffset(result);
                        consumer.Commit();
                        throw new TypeDefinitionException(result.Message.Value.MessageType);
                    }

                    _consumerHandler = scope.ServiceProvider.GetRequiredService<IOutboxMessageHandler<Tk>>();

                    if (_consumerHandler == null)
                    {
                        throw new ArgumentNullException(nameof(_consumerHandler), "Could not find event handler");
                    }

                    var message = JsonConvert.DeserializeObject(result.Message.Value.Payload, type);
                    var handlerMethod = _consumerHandler.GetType().GetMethod("On", new Type[] { typeof(Tk), message!.GetType() });

                    if (handlerMethod == null)
                    {
                        throw new ArgumentNullException(nameof(handlerMethod), "Could find event handler method");
                    }


                    dynamic awaitable = handlerMethod.Invoke(_consumerHandler, new object[] { result.Message.Key, message });
                    await awaitable;

                    consumer.StoreOffset(result);
                }
            }
        }
    }
}
