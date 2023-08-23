using Identity.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Identity.API
{
    public static class DependencyInjection
    {
        public static void ConfigureAPI(this IServiceCollection services) 
        {
            services.AddControllers();
        }

        public static async Task ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<IdentityContext>();

            await dbContext?.Database.MigrateAsync();
        }
    }
}
