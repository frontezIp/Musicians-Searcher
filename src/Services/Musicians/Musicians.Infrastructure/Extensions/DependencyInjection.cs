using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Infrastructure.Kafka.BackgroundConsumers;
using Musicians.Infrastructure.Kafka.ConfigOptions;
using Musicians.Infrastructure.Kafka.ConsumerHandlers;
using Musicians.Infrastructure.Kafka.ConsumerHandlers.Handlers;
using Musicians.Infrastructure.Persistance.Contexts;
using Musicians.Infrastructure.Persistance.Options;
using Musicians.Infrastructure.Persistance.Repositories;
using Shared.Messages.IdentityMessages;

namespace Musicians.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureMongo(configuration);
            services.ConfigureRepositories();
            services.ConfigureContext();
            services.ConfigureOptions(configuration);
            services.ConfigureMessageOutboxConsumers(configuration);
            services.ConfigureMessageHandlers();
        }

        private static void ConfigureMongo(this IServiceCollection services, IConfiguration configuration)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));

            services.AddSingleton(serviceProvider =>
            {
                var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoDbSettings!.ConnectionString);
                return mongoClient.GetDatabase(mongoDbSettings.Database);
            });
        }

        private static void ConfigureMessageHandlers(this IServiceCollection services)
        {
            services.AddScoped<IOutboxMessageHandler<string>, OutboxMessageHandler<string>>();
            services.AddScoped<IConsumerHandler<string, UserCreatedMessage>, UserCreatedMessageHandler>();
        }

        private static void ConfigureMessageOutboxConsumers(this IServiceCollection services, IConfiguration configuration)
        {
            var topicOptions = new KafkaTopicOptions();
            configuration.GetSection(nameof(KafkaTopicOptions)).Bind(topicOptions);

            services.AddHostedService<OutboxMessagesBackgroundConsumer<string>>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<OutboxMessagesBackgroundConsumer<string>>>();
                var consumerConfig = provider.GetRequiredService<IOptions<ConsumerConfig>>();
                return new OutboxMessagesBackgroundConsumer<string>(
                    topicOptions.IdentityTopic,
                    provider,
                    consumerConfig,
                    logger);
            });
        }

        private static void ConfigureContext(this IServiceCollection services)
        {
            services.AddScoped<MusiciansContext>();
        }

        private static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGenresRepository, GenresRepository>();
            services.AddScoped<ISkillsRepository, SkillsRepository>();
            services.AddScoped<IMusiciansRepository, MusiciansRepository>();
            services.AddScoped<IFriendsRepository, FriendsRepository>();
            services.AddScoped<ISubcribersRepository, SubscribersRepository>();
        }
    }
}
