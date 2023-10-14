using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;

namespace Musicians.API.Extensions
{
    public static class DependencyInjection
    {
        public static void ConfigureAPI(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureControllers();
            services.ConfigureVersioning();
            services.ConfigureCors();
            services.ConfigureAuthentication(configuration);
            services.ConfigureAuthorization();
            services.ConfigureSwagger(configuration);

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

        private static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
            });
        }
    }
}
