using MongoDB.Driver;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Infrastructure.Persistance.Contexts;
using Musicians.Infrastructure.Seed;

namespace Musicians.API.Extensions
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(this WebApplication application)
        {
            using (var scope = application.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MusiciansContext>();

                var genresRepository = scope.ServiceProvider.GetRequiredService<IGenresRepository>();

                var skillsRepository = scope.ServiceProvider.GetRequiredService<ISkillsRepository>();

                if (!context.GetGenres.AsQueryable().Any())
                    await context.GetGenres.InsertManyAsync(DataToSeed.GetGenres());

                if (!context.GetSkills.AsQueryable().Any())
                    await context.GetSkills.InsertManyAsync(DataToSeed.GetSkills());

                if (!context.GetMusicians.AsQueryable().Any())
                    await context.GetMusicians.InsertManyAsync(DataToSeed.GetMusicians());
            }
        }
    }
}
