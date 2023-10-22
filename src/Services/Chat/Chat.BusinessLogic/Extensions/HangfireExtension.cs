using Chat.BusinessLogic.HangfireServices.Implementations;
using Chat.BusinessLogic.HangfireServices.Interfaces;
using Chat.BusinessLogic.Options.HangfireOptions;
using Chat.BusinessLogic.Services.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.BusinessLogic.Extensions
{
    public static class HangfireExtension
    {
        public static void ConfigureHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("sqlConnection");
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseSerilogLogProvider()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString));

            services.ConfigureHangfireServices();

            services.AddHangfireServer();
        }

        public static void ConfigureHangfireServices(this IServiceCollection services)
        {
            services.AddScoped<IHangfireMessageService, HangfireMessageService>();
        }

        public static void UseHangfireRecurringJobs(this IApplicationBuilder applicationBuilder, IConfiguration configuration)
        {
            var jobManager = applicationBuilder.ApplicationServices.GetRequiredService<IRecurringJobManager>();

            var hangfireOptions = new HangfireOptions();

            configuration.GetSection(nameof(HangfireOptions)).Bind(hangfireOptions);

            jobManager.AddOrUpdate<IChatRoomService>(hangfireOptions.DeleteChatRoomsOptions.JobId,
                service => service.DeleteEmptyChatRoomsAsync(CancellationToken.None),
                Cron.Minutely);
        }
    }
}
