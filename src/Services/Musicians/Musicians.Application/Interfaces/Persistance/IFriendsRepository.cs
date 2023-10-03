using Musicians.Domain.Models;
namespace Musicians.Application.Interfaces.Persistance
{
    public interface IFriendsRepository
    {
        Task AddToFriendsAsync(Guid musicianId, Guid friendId);
        Task DeleteFromFriendsAsync(Guid musicianId, Guid friendId);
        Task<IEnumerable<Musician>> GetAllMusicianFriendsAsync(Guid musicianId, CancellationToken cancellationToken = default);
        Task<bool> IsAlreadyFriendAsync(Guid musicianId, Guid possibleFriend);
    }
}
