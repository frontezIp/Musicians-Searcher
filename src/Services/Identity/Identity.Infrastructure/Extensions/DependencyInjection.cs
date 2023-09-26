using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Identity.Infrastructure.Persistance.Contexts;
using Identity.Application.Interfaces.Persistance;
using Identity.Infrastructure.Persistance.Repositories;
using Confluent.Kafka;
using Identity.Application.Interfaces.MessageBroker.Producer;
using Identity.Infrastructure.MessageBroker.Producers;
using Identity.Infrastructure.MessageBroker.TranscationalOutbox;
using Identity.Infrastructure.MessageBroker.BackgroudProducers;

namespace Identity.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigurePostgresContext(configuration);
            services.ConfigureRepositories();
            services.ConfigureOptions(configuration);
            services.ConfigureEventProducers();
        }

        private static void ConfigureRepositories(this IServiceCollection services) 
        {
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();
        }

        private static void ConfigurePostgresContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("postgresConnection")));
        }

        private static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ProducerConfig>(configuration.GetSection(nameof(ProducerConfig)));
        }

        private static void ConfigureEventProducers(this IServiceCollection services)
        {
            services.AddScoped<IEventProducer, EventProducer>();
            services.AddScoped<ITransactionalEventProducer, TransactionalEventProducer>();
            services.AddHostedService<BackgroudOutboxProducer>();
        }
    }
}
