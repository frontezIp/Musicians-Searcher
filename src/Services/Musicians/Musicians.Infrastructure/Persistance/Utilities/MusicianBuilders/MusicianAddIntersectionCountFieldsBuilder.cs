using MongoDB.Bson;
using Musicians.Domain.RequestParameters;

namespace Musicians.Infrastructure.Persistance.Utilities.MusicianBuilders
{
    public static class MusicianAddIntersectionCountFieldsBuilder
    {
        public static BsonDocument AddFields(MusicianParameters parameters)
        {
            var addIntersectionCountFieldsCommands = new BsonDocument("$addFields", new BsonDocument
            {
                 {"SkillsIntersectionCount", AddSkillsIntersectionCountField(parameters) },
                 {"GenresIntersectionCount", AddGenresIntersectionCountField(parameters) }
            });

            return addIntersectionCountFieldsCommands;
        }

        public static BsonDocument AddSkillsIntersectionCountField(MusicianParameters parameters)
        {
            var skillsDocument = new BsonDocument("$size", new BsonDocument("$setIntersection", new BsonArray
            {
                    "$skills._id",
                    new BsonArray(parameters.SkillsIds.Select(guidId => guidId.ToString()))
            }));

            return skillsDocument;
        }

        public static BsonDocument AddGenresIntersectionCountField(MusicianParameters parameters)
        {
            var genresDocument = new BsonDocument("$size", new BsonDocument("$setIntersection", new BsonArray
                {
                    "$favourite_genres._id",
                    new BsonArray(parameters.GenresIds.Select(guidId => guidId.ToString()))
                }));
            return genresDocument;
        }
    }
}
