using Identity.Infrastructure.Persistance.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure.Extensions
{
    public static class DatabaseConfigurationExtension
    {
        public static async Task ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<IdentityContext>();

            await dbContext?.Database.MigrateAsync();
        }
    }
}
