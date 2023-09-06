using MongoDB.Driver;
using Musicians.Domain.RequestParameters;
using Musicians.Infrastructure.Models;

namespace Musicians.Infrastructure.Persistance.Utilities.MusicianBuilders
{
    public static class MusicianSortBuilder
    {
        public static SortDefinition<Musician> BuildSort(MusicianParameters musicianParameters)
        {
            var builder = Builders<Musician>.Sort;

            return builder.Combine(BuildSkillsIntersectionSort(builder, musicianParameters), BuildGenresIntersectionSort(builder, musicianParameters));
        }

        private static SortDefinition<Musician> BuildGenresIntersectionSort(SortDefinitionBuilder<Musician> sortDefinitionBuilder, MusicianParameters musicianParameters)
        {
            return sortDefinitionBuilder.Descending("GenresIntersectionCount");
        }


        private static SortDefinition<Musician> BuildSkillsIntersectionSort(SortDefinitionBuilder<Musician> sortDefinitionBuilder, MusicianParameters musicianParameters)
        {
            return sortDefinitionBuilder.Descending("SkillsIntersectionCount");
        }
    }
}
