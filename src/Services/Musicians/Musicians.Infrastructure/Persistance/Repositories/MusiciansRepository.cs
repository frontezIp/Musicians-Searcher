using MongoDB.Driver;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Infrastructure.Models;
using Musicians.Infrastructure.Persistance.Contexts;
using Musicians.Domain.RequestParameters;
using Musicians.Infrastructure.Persistance.Utilities.MusicianBuilders;
using Musicians.Domain.RequestFeatures;
using MongoDB.Bson.Serialization;

namespace Musicians.Infrastructure.Persistance.Repositories
{
    internal class MusiciansRepository : 
        BaseRepository<Musician>, IMusiciansRepository
    {
        public MusiciansRepository(MusiciansContext musiciansContext) : base(musiciansContext.GetCollection<Musician>())
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

            var pipeline = await _collection
                .Aggregate()
                .Match(MusicianFiltersBuilder.BuildFilter(parameters))
                .AppendStage<Musician>(MusicianAddIntersectionCountFieldsBuilder.AddFields(parameters))
                .Sort(MusicianSortBuilder.BuildSort(parameters))
                .Project(MusicianProjectionBuilder.BuildProjection())
                .Skip(skip)
                .Limit(take)
                .ToListAsync(cancellationToken);

            var res = pipeline.Select(doc => BsonSerializer.Deserialize<Musician>(doc)).ToList();

            return new PagedList<Musician>(res, res.Count,
                parameters.PageNumber, parameters.PageSize);
        }
    }
}
