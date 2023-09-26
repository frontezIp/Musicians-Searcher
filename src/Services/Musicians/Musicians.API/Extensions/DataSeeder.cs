using MongoDB.Driver;
using Musicians.Domain.Models;
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

                if (!context.GetCollection<Genre>().AsQueryable().Any())
                    await context.GetCollection<Genre>().InsertManyAsync(DataToSeed.GetGenres());

                if (!context.GetCollection<Skill>().AsQueryable().Any())
                    await context.GetCollection<Skill>().InsertManyAsync(DataToSeed.GetSkills());

                if (!context.GetCollection<Musician>().AsQueryable().Any())
                    await context.GetCollection<Musician>().InsertManyAsync(DataToSeed.GetMusicians());
            }
        }
    }
}
