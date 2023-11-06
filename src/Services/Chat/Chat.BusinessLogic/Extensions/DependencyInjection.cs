using Chat.BusinessLogic.Helpers.Implementations;
using Chat.BusinessLogic.Helpers.Interfaces;
using Chat.BusinessLogic.Kafka.BackgroundConsumers;
using Chat.BusinessLogic.Kafka.ConfigOptions;
using Chat.BusinessLogic.Kafka.ConsumerHandlers.Handlers;
using Chat.BusinessLogic.Kafka.ConsumerHandlers;
using Chat.BusinessLogic.Services.Implementations;
using Chat.BusinessLogic.Services.Interfaces;
using Chat.DataAccess.Options;
using Confluent.Kafka;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared.Messages.IdentityMessages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Chat.BusinessLogic.Grpc.Interceptors;
using Chat.BusinessLogic.Grpc.v1.Clients.Implementations;
using Chat.BusinessLogic.Grpc.v1.Clients.Interfaces;
using Chat.BusinessLogic.Options;
using Musicians.Application.Grpc.v1;
using Chat.BusinessLogic.Options.HangfireOptions;
using Chat.BusinessLogic.Options.RedisOptions;

namespace Chat.BusinessLogic.Extensions
{
    public static class DependencyInjection
    {
        public static void ConfigureBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMappings();
            services.ConfigureFluentValidators();
            services.ConfigureServiceHelpers();
            services.ConfigureServices();
            services.ConfigureOptions(configuration);
            services.ConfigureMessageHandlers();
            services.ConfigureMessageOutboxConsumers(configuration);
            services.ConfigureGrpcClient();
            services.ConfigureHangfire(configuration);
        }

        private static void AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(BusinessLogicAssemblyReference.Assembly);
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
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

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IChatParticipantService, ChatParticipantService>();
            services.AddScoped<IChatRoomService, ChatRoomService>();    
            services.AddScoped<IChatRoleService, ChatRoleService>();    
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IMessengerUserService, MessengerUserService>();
        }

        private static void ConfigureServiceHelpers(this IServiceCollection services)
        {
            services.AddScoped<IChatParticipantServiceHelper, ChatParticipantServiceHelper>();
            services.AddScoped<IMessageServiceHelper, MessageServiceHelper>();
            services.AddScoped<IMessengerUserServiceHelper, MessengerUserServiceHelper>();
            services.AddScoped<IChatRoomServiceHelper, ChatRoomServiceHelper>();
            services.AddScoped<IChatRoleServiceHelper, ChatRoleServiceHelper>();
        }


        private static void ConfigureGrpcClient(this IServiceCollection services)
        {
            services.AddScoped<ErrorInterceptor>();
            services.AddScoped<IMusicianClient, MusicianClient>();
            services.AddGrpcClient<Musician.MusicianClient>((p, o) =>
            {
                var grpcConfig = p.GetRequiredService<IOptions<GrpcConfigOptions>>();
                o.Address = new Uri(grpcConfig.Value.MusicianUrl);
            }).AddInterceptor<ErrorInterceptor>();
        }

        private static void ConfigureConsumers(this IServiceCollection services, IConfiguration configuration)
        {
            var topicOptions = new KafkaTopicOptions();
            configuration.GetSection(nameof(KafkaTopicOptions)).Bind(topicOptions);
            services.AddKafkaConsumer<string, UserCreatedMessage, UserCreatedMessageHandler>(o =>
            {
                o.Topic = topicOptions.IdentityTopic;
            });
        }

        private static void AddKafkaConsumer<Tk, Tv, THandler>(this IServiceCollection services,
            Action<KafkaTopicSelectionOption<Tk, Tv>> configAction)
            where Tv : class
            where THandler : class, IConsumerHandler<Tk, Tv>
        {
            services.AddScoped<IConsumerHandler<Tk, Tv>, THandler>();

            services.AddHostedService<MessagesBackgroundConsumer<Tk, Tv>>();

            services.Configure(configAction);
        }

        private static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ChatRolesOptions>(configuration.GetSection(nameof(ChatRolesOptions)));
            services.Configure<ConsumerConfig>(configuration.GetSection(nameof(ConsumerConfig)));
            services.Configure<KafkaTopicOptions>(configuration.GetSection(nameof(KafkaTopicOptions)));
            services.Configure<GrpcConfigOptions>(configuration.GetSection(nameof(GrpcConfigOptions)));
            services.Configure<HangfireOptions>(configuration.GetSection(nameof(HangfireOptions)));
            services.Configure<RedisOptions>(configuration.GetSection(nameof(RedisOptions)));
        }

        private static void ConfigureFluentValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(BusinessLogicAssemblyReference.Assembly);
        }
    }
}
