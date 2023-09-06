using MongoDB.Driver;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Infrastructure.Models;
using Musicians.Infrastructure.Persistance.Contexts;
using Musicians.Domain.RequestParameters;
using Musicians.Infrastructure.Persistance.Utilities.MusicianBuilders;
using Musicians.Domain.RequestFeatures;
using MongoDB.Bson;

namespace Musicians.Infrastructure.Persistance.Repositories
{
    internal class MusiciansRepository : 
        BaseRepository<Musician>, IMusiciansRepository
    {
        public MusiciansRepository(MusiciansContext musiciansContext) : base(musiciansContext.GetMusicians)
        {
        }

        public async Task UpdateMusicianProfileAsync(Musician musicianToUpdate)
        {
            await _collection.UpdateOneAsync(musician => musician.Id == musicianToUpdate.Id, MusicianProfileUpdateBuilder.BuildUpdate(musicianToUpdate));
        }

        public async Task<PagedList<Musician>> GetFilteredMusiciansAsync(MusicianParameters parameters, CancellationToken cancellationToken = default)
        {
            var skip = (parameters.PageNumber - 1) * parameters.PageSize;
            var take = parameters.PageSize;

            var pipeline = new IPipelineStageDefinition[]
            {
                new BsonDocumentPipelineStageDefinition<Musician, Musician>(
                    new BsonDocument("$match", MusicianFiltersBuilder.BuildFilter(parameters).Render(_collection.DocumentSerializer, _collection.Settings.SerializerRegistry))),
                new BsonDocumentPipelineStageDefinition<Musician, Musician>(MusicianAddIntersectionCountFieldsBuilder.AddFields(parameters)),
                new BsonDocumentPipelineStageDefinition<Musician,Musician>(
                    new BsonDocument("$sort", new BsonDocument
                    {
                        { "SkillsIntersectionCount", -1 },
                        { "GenresIntersectionCount", -1 }
                    })),
                new BsonDocumentPipelineStageDefinition<Musician, Musician>(
                    new BsonDocument("$project", MusicianProjectionBuilder.BuildProjection().Render(_collection.DocumentSerializer, _collection.Settings.SerializerRegistry))),
                new BsonDocumentPipelineStageDefinition<Musician, Musician>(
                    new BsonDocument("$skip", skip)),
                new BsonDocumentPipelineStageDefinition<Musician,Musician>(
                    new BsonDocument("$limit", take))
            };

            var res = await _collection.Aggregate<Musician>(pipeline).ToListAsync(cancellationToken);

            return new PagedList<Musician>(res, res.Count,
                parameters.PageNumber, parameters.PageSize);
        }
    }
}
