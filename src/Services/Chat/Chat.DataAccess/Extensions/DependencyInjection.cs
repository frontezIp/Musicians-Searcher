using Chat.DataAccess.Contexts;
using Chat.DataAccess.Repositories.Implementations;
using Chat.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.DataAccess.Extensions
{
    public static class DependencyInjection
    {
        public static void ConfigureDAL(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSqlContext(configuration);
            services.ConfigureRepositories();
        }

        private static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChatContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        }

        private static void ConfigureRepositories(this IServiceCollection services)
        {
            services.AddScoped<IChatParticipantRepository, ChatParticipantRepository>();
            services.AddScoped<IChatRoleRepository, ChatRoleRepository>();
            services.AddScoped<IChatRoomRepository, ChatRoomRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessengerUserRepository, MessengerUserRepository>();
        }
    }
}
