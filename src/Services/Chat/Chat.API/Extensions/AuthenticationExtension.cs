using Chat.BusinessLogic.Options.SignalROptions;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Chat.API.Extensions
{
    public static class AuthenticationExtension
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddJwtBearer("Bearer", options =>
            {
                options.RequireHttpsMetadata = false;
                options.Audience = "Chat";
                options.Authority = configuration["IdentityServer:IssuerUri"];
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        var signalOptions = new SignalRConfigOptions();
                        configuration.GetSection(nameof(SignalRConfigOptions)).Bind(signalOptions);
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments($"/{signalOptions.ChatHubName}")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}
