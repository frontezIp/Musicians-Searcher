using MongoDB.Driver;

namespace Musicians.Infrastructure.Persistance.Contexts
{
    public class MusiciansContext
    {
        public IMongoDatabase Database { get; init; }

        public MusiciansContext(IMongoDatabase mongoDatabase)
        {
            Database = mongoDatabase;
        }

        public IMongoCollection<T> GetCollection<T>() where T : class 
        {
            return Database.GetCollection<T>(typeof(T).Name);
        }

    }
}
