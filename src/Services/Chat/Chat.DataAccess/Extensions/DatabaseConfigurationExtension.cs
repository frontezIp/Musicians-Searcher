using Chat.DataAccess.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Chat.DataAccess.Extensions
{
    public static class DatabaseConfigurationExtension
    {
        public static async Task ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetRequiredService<ChatContext>();

            await dbContext!.Database.MigrateAsync();
        }
    }
}
