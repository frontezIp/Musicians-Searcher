using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Extensions
{
    public static class DatabaseConfigurationExtension
    {
        public static async Task ApplyMigration<TDbContext>(this IApplicationBuilder application)
            where TDbContext : DbContext
        {
            using var services = application.ApplicationServices.CreateScope();

            var migrator = services.ServiceProvider.GetService<IMigrator>();

            await migrator!.MigrateAsync(null, CancellationToken.None);
        }
    }
}
