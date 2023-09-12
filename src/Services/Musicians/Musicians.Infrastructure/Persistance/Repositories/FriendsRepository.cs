using MongoDB.Driver;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Infrastructure.Models;
using Musicians.Infrastructure.Persistance.Contexts;

namespace Musicians.Infrastructure.Persistance.Repositories
{
    internal class FriendsRepository :
        BaseRepository<Musician>, IFriendsRepository
    {
        public FriendsRepository(MusiciansContext musiciansContext) : base(musiciansContext.GetCollection<Musician>())
        {
        }

        public async Task AddToFriendsAsync(Guid musicianId, Guid friendId)
        {
            var filter = _filterBuilder.Eq(musician => musician.Id, musicianId);
            var update = _updateBuilder.Push(musician => musician.Friends, friendId)
                         .Inc(musician => musician.FriendsCount, 1); ;

            await _collection.UpdateOneAsync(filter, update, null);
        }

        public async Task DeleteFromFriendsAsync(Guid musicianId, Guid friendId)
        {
            var filter = _filterBuilder.Eq(musician => musician.Id, musicianId);

            var update = _updateBuilder.Pull(musician => musician.Friends, friendId)
                                        .Inc(musician => musician.FriendsCount, -1);

            await _collection.UpdateOneAsync(filter, update, null);
        }

        public async Task<IEnumerable<Musician>> GetAllMusicianFriendsAsync(Guid musicianId, CancellationToken cancellationToken = default)
        {
            var query = GetByCondition(musician => musician.Id == musicianId)
                .SelectMany(musician => musician.Friends);

            var friends = await _collection.Find(musician => query.Contains(musician.Id)).ToListAsync(cancellationToken);

            return friends;
        }

        public async Task<bool> IsAlreadyFriendAsync(Guid musicianId, Guid possibleFriend)
        {
            var musician = await GetByCondition(musician => musician.Id == musicianId)
                .SingleOrDefaultAsync();

            return musician.Friends.Any(friend => friend.Equals(possibleFriend));
        }
    }
}
