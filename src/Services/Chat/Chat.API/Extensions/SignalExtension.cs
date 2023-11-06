using Chat.API.SignalR.Hubs;
using Chat.API.SignalR.Providers;
using Chat.BusinessLogic.Options.SignalROptions;
using Microsoft.AspNetCore.SignalR;

namespace Chat.API.Extensions
{
    public static class SignalExtension
    {
        public static void ConfigureSignalR(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, UserIdProvider>();
        }

        public static void MapHubs(this WebApplication app)
        {
            var signalOptions = new SignalRConfigOptions();
            app.Configuration.GetSection(nameof(SignalRConfigOptions)).Bind(signalOptions);
            app.MapHub<ChatHub>($"/{signalOptions.ChatHubName}");
        }
    }
}
