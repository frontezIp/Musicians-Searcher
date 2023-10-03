using Musicians.Domain.Models;

namespace Musicians.Application.Interfaces.Persistance
{
    public interface ISubcribersRepository
    {
        Task AddToSubcribersAsync(Guid musicianId, Guid subscriberId);
        Task DeleteFromSubscribersAsync(Guid musicianId, Guid subscriberId);
        Task<IEnumerable<Musician>> GetAllMusicianSubscribersAsync(Guid musicianId, CancellationToken cancellationToken = default);
        Task<bool> IsAlreadySubscriberAsync(Guid musicianToSubscribeId, Guid musicianId);
    }
}
