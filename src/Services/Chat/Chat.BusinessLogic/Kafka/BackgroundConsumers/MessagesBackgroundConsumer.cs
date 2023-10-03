using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Chat.BusinessLogic.Kafka.ConfigOptions;
using Chat.BusinessLogic.Kafka.ConsumerHandlers;

namespace Chat.BusinessLogic.Kafka.BackgroundConsumers
{
    internal class MessagesBackgroundConsumer<Tk, Tv> : BackgroundService
        where Tv : class
    {
        private readonly IOptions<KafkaTopicSelectionOption<Tk, Tv>> _topicSelection;
        private IConsumerHandler<Tk, Tv> _consumerHandler;
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<ConsumerConfig> _consumerConfig;
        private readonly ILogger<MessagesBackgroundConsumer<Tk,Tv>> _logger;

        public MessagesBackgroundConsumer(IOptions<KafkaTopicSelectionOption<Tk, Tv>> topicSelection,
            IServiceProvider serviceProvider,
            ILogger<MessagesBackgroundConsumer<Tk, Tv>> logger,
            IOptions<ConsumerConfig> consumerConfig)
        {
            _topicSelection = topicSelection;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _consumerConfig = consumerConfig;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            _logger.LogInformation("Consumer of the {MessageName} messages has started", nameof(Tv));

            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                _consumerHandler = scope.ServiceProvider.GetRequiredService<IConsumerHandler<Tk, Tv>>();

                var builder = new ConsumerBuilder<Tk, Tv>(_consumerConfig.Value)
                    .SetValueDeserializer(new KafkaDeserializer<Tv>());

                using var consumer = builder.Build();

                consumer.Subscribe(_topicSelection.Value.Topic);

                while (!stoppingToken.IsCancellationRequested)
                {
                    var result = consumer.Consume(stoppingToken);

                    if (result == null)
                    {
                        continue;
                    }

                    await _consumerHandler.HandleAsync(result.Message.Key, result.Message.Value);

                    consumer.StoreOffset(result);
                }
            }
        }
    }
}
