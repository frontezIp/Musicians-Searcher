using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Musicians.Domain.Models;
using Musicians.Infrastructure.Models;
using Musicians.Infrastructure.Persistance.Options;

namespace Musicians.Infrastructure.Persistance.Contexts
{
    public class MusiciansContext
    {
        public IMongoDatabase Database { get; init; }
        private readonly IOptions<CollectionsNamesOptions> _options;

        public MusiciansContext(IOptions<CollectionsNamesOptions> options, IMongoDatabase mongoDatabase)
        {
            Database = mongoDatabase;
            _options = options;
        }

        public IMongoCollection<Musician> GetMusicians
        {
            get
            {
                return Database.GetCollection<Musician>(_options.Value.MusiciansCollectionName);
            }
        }

        public IMongoCollection<Genre> GetGenres
        {
            get
            {
                return Database.GetCollection<Genre>(_options.Value.GenresCollectionName);
            }
        }

        public IMongoCollection<Skill> GetSkills
        {
            get
            {
                return Database.GetCollection<Skill>(_options.Value.SkillsCollectionName);
            }
        }
    }
}
