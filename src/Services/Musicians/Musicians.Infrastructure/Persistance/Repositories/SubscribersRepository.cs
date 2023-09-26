using MongoDB.Driver;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Domain.Models;
using Musicians.Infrastructure.Persistance.Contexts;

namespace Musicians.Infrastructure.Persistance.Repositories
{
    public class SubscribersRepository :
        BaseRepository<Musician>, ISubcribersRepository
    {
        public SubscribersRepository(MusiciansContext context) : base(context.GetCollection<Musician>())
        {
        }

        public async Task<IEnumerable<Musician>> GetAllMusicianSubscribersAsync(Guid musicianId, CancellationToken cancellationToken = default)
        {
            var query = GetByCondition(musician => musician.Id == musicianId)
                .SelectMany(musician => musician.Subscribers);

            var subscribers = await _collection.Find(musician => query.Contains(musician.Id)).ToListAsync(cancellationToken);

            return subscribers;
        }

        public async Task AddToSubcribersAsync(Guid musicianId, Guid subscriberId)
        {
            var filter = _filterBuilder.Eq(musician => musician.Id, musicianId);
            var update = _updateBuilder.Push(musician => musician.Subscribers, subscriberId)
                         .Inc(musician => musician.SubscribersCount, 1);

            await _collection.UpdateOneAsync(filter, update, null);
        }

        public async Task DeleteFromSubscribersAsync(Guid musicianId, Guid subscriberId)
        {
            var filter = _filterBuilder.Eq(musician => musician.Id, musicianId);
            var update = _updateBuilder.Pull(musician => musician.Subscribers, subscriberId)
                         .Inc(musician => musician.SubscribersCount, -1); ;

            await _collection.UpdateOneAsync(filter, update, null);
        }

        public async Task<bool> IsAlreadySubscriberAsync(Guid musicianToSubscribeId, Guid musicianId)
        {
            var musician = await GetByCondition(musician => musician.Id == musicianToSubscribeId)
                .SingleOrDefaultAsync();

            return musician.Subscribers.Any(subscriber => subscriber.Equals(musicianId));
        }
    }
}
