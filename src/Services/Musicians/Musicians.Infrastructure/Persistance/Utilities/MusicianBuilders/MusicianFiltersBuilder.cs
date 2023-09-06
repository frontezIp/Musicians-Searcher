using MongoDB.Driver;
using Musicians.Infrastructure.Models;
using Musicians.Domain.RequestParameters;
using Shared.Enums;

namespace Musicians.Infrastructure.Persistance.Utilities.MusicianBuilders
{
    public static class MusicianFiltersBuilder
    {
        public static FilterDefinition<Musician> BuildFilter(MusicianParameters musicianParameters)
        {
            var builder = Builders<Musician>.Filter;

            return builder.And(BuildAgeFilter(builder, musicianParameters),
                BuildNameFilter(builder, musicianParameters),
                BuildCountryFilter(builder, musicianParameters),
                BuildCityFilter(builder, musicianParameters),
                BuildGenresFilter(builder, musicianParameters),
                BuildSkillsFilter(builder, musicianParameters),
                BuildSexTypeFilter(builder, musicianParameters));
        }

        private static FilterDefinition<Musician> BuildAgeFilter(FilterDefinitionBuilder<Musician> builder, MusicianParameters musicianParameters)
        {
            return builder.And(builder.Gte(musician => (ulong)musician.Age, musicianParameters.MinAge), builder.Lte(musician => (uint)musician.Age, musicianParameters.MaxAge));
        }

        private static FilterDefinition<Musician> BuildNameFilter(FilterDefinitionBuilder<Musician> builder, MusicianParameters musicianParameters)
        {
            return string.IsNullOrEmpty(musicianParameters.SearchTerm) ?
                builder.Empty :
                builder.Where(musician => musician.Username.Contains(musicianParameters.SearchTerm) || musician.FullName.Contains(musicianParameters.SearchTerm));
        }

        private static FilterDefinition<Musician> BuildCountryFilter(FilterDefinitionBuilder<Musician> builder, MusicianParameters musicianParameters)
        {
            return string.IsNullOrEmpty(musicianParameters.Country) ?
                builder.Empty :
                builder.Where(musician => musician.Location.Contains(musicianParameters.Country));
        }

        private static FilterDefinition<Musician> BuildCityFilter(FilterDefinitionBuilder<Musician> builder, MusicianParameters musicianParameters)
        {
            return string.IsNullOrEmpty(musicianParameters.City) ?
                builder.Empty :
                builder.Where(musician => musician.Location.Contains(musicianParameters.City));
        }

        private static FilterDefinition<Musician> BuildGenresFilter(FilterDefinitionBuilder<Musician> builder, MusicianParameters musicianParameters)
        {
            return musicianParameters.GenresIds.Count == 0 ?
                builder.Empty :
                builder.Where(musician => musician.FavouriteGenres.Any(genre => musicianParameters.GenresIds.Contains(genre.Id)));
        }

        private static FilterDefinition<Musician> BuildSkillsFilter(FilterDefinitionBuilder<Musician> builder, MusicianParameters musicianParameters)
        {
            return musicianParameters.SkillsIds.Count == 0 ?
                builder.Empty :
                builder.Where(musician => musician.Skills.Any(skill => musicianParameters.SkillsIds.Contains(skill.Id)));
        }

        private static FilterDefinition<Musician> BuildSexTypeFilter(FilterDefinitionBuilder<Musician> builder, MusicianParameters musicianParameters)
        {
            return Enum.IsDefined(typeof(SexTypes), musicianParameters.SexType) ?
                builder.Where(musician => musician.Sex.Equals(musicianParameters.SexType)) :
                builder.Empty;
        }
    }
}
