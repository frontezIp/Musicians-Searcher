using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Identity.Infrastructure.Persistance.Contexts;
using Identity.Application.Interfaces.Persistance;
using Identity.Infrastructure.Persistance.Repositories;

namespace Identity.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigurePostgresContext(configuration);
            services.ConfigureRepositories();
        }

        private static void ConfigureRepositories(this IServiceCollection services) 
        {
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
        }

        private static void ConfigurePostgresContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("postgresConnection")));
        }
    }
}
