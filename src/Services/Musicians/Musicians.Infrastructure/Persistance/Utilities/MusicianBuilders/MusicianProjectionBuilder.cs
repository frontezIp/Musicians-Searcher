using MongoDB.Driver;
using Musicians.Domain.Models;

namespace Musicians.Infrastructure.Persistance.Utilities.MusicianBuilders
{
    public static class MusicianProjectionBuilder
    {
        public static ProjectionDefinition<Musician> BuildProjection()
        {
            var builder = Builders<Musician>.Projection;

            var subscribersExcludedProjection = builder.Exclude(musician => musician.Subscribers);

            var friendsExcludedProjection = builder.Exclude(musician => musician.Friends);

            return builder.Combine(subscribersExcludedProjection, friendsExcludedProjection);
        }
    }
}
