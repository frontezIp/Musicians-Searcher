using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Infrastructure.Persistance.Contexts;
using Musicians.Infrastructure.Persistance.Options;
using Musicians.Infrastructure.Persistance.Repositories;
using Musicians.Infrastructure.Persistance.Utilities;

namespace Musicians.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureMongo(configuration);
            services.ConfigureRepositories();
            services.ConfigureContext();
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
