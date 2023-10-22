using Identity.API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;

namespace Identity.API
{
    public static class DependencyInjection
    {
        public static void ConfigureAPI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.ConfigureControllers();
            services.ConfigureVersioning();
            services.ConfigureCors();
            services.ConfigureSwagger(configuration);
            services.ConfigureIdentity(configuration);
        }

        private static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });
        }

        private static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
            });
        }
    }
}