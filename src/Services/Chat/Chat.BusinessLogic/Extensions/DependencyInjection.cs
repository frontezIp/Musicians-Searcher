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
        }

        private static void AddMappings(this IServiceCollection services)
        {
            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(BusinessLogicAssemblyReference.Assembly);
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
        }

        private static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IChatParticipantService, ChatParticipantService>();
            services.AddScoped<IChatRoomService, ChatRoomService>();    
            services.AddScoped<IChatRoleService, ChatRoleService>();    
            services.AddScoped<IMessageService, MessageService>();  
        }

        private static void ConfigureServiceHelpers(this IServiceCollection services)
        {
            services.AddScoped<IChatParticipantServiceHelper, ChatParticipantServiceHelper>();
            services.AddScoped<IMessageServiceHelper, MessageServiceHelper>();
            services.AddScoped<IMessengerUserServiceHelper, MessengerUserServiceHelper>();
            services.AddScoped<IChatRoomServiceHelper, ChatRoomServiceHelper>();
            services.AddScoped<IChatRoleServiceHelper, ChatRoleServiceHelper>();
        }

        private static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ChatRolesOptions>(configuration.GetSection(nameof(ChatRolesOptions)));
        }

        private static void ConfigureFluentValidators(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(BusinessLogicAssemblyReference.Assembly);
        }
    }
}
