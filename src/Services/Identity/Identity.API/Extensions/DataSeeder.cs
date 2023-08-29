using Identity.Domain.Models;
using Identity.Infrastructure.Persistance.Contexts;
using Identity.Infrastructure.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Constants;

namespace Identity.API.Extensions
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<IdentityContext>();
                using var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                using var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

                if(!context.Countries.AsNoTracking().Any())
                {
                    var countries = DataToSeed.GetCountriesToAdd();
                    var cities = DataToSeed.GetCitiesToAdd();

                    await context.Countries.AddRangeAsync(countries);

                    await context.Cities.AddRangeAsync(cities);

                    await context.SaveChangesAsync();
                }

                if (!context.Roles.AsNoTracking().Any())
                {
                    var roles = DataToSeed.GetRolesToAdd(); 

                    await context.Roles.AddRangeAsync(roles);

                    await context.SaveChangesAsync();
                }

                if (!context.Users.AsNoTracking().Any())
                {
                    var user = DataToSeed.GetAdminUserToAdd();

                    await userManager.CreateAsync(user.user, user.Password);

                    await userManager.AddToRoleAsync(user.user, RoleNamesConstants.AdminRoleName);

                    await context.SaveChangesAsync();
                }


            }
        }
    }
}
