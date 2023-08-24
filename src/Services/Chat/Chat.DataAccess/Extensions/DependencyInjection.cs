using Chat.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DataAccess.Extensions
{
    public static class DependencyInjection
    {
        public static void ConfigureDAL(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSqlContext(configuration);
        }

        private static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ChatContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        }

    }
}
