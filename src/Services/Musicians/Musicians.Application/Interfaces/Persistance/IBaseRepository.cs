using MongoDB.Driver;

namespace Musicians.Application.Interfaces.Persistance
{
    public interface IBaseRepository<T>
    {
        Task CreateAsync(T entity, CancellationToken cancellationToken = default, InsertOneOptions? options = null);
        Task DeleteAsync(T entity, CancellationToken cancellationToken = default, DeleteOptions? deleteOptions = null);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default);
        Task UpdateAsync(T entity, CancellationToken cancellationToken = default, ReplaceOptions? replaceOptions = null);
    }
}
