using MongoDB.Driver;
using Musicians.Infrastructure.Models;

namespace Musicians.Infrastructure.Persistance.Utilities.MusicianBuilders
{
    public static class MusicianProfileUpdateBuilder
    {
        public static UpdateDefinition<Musician> BuildUpdate(Musician updatedMusician)
        {
            var builder = Builders<Musician>.Update;

            var goalUpdate = builder.Set(musician => musician.Goal, updatedMusician.Goal);
            var genresUpdate = builder.Set(musician => musician.FavouriteGenres, updatedMusician.FavouriteGenres);
            var skillsUpdate = builder.Set(musician => musician.Skills, updatedMusician.Skills);

            return builder.Combine(goalUpdate, genresUpdate, skillsUpdate);
        }
    }
}
