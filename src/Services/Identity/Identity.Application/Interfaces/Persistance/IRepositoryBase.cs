using System.Linq.Expressions;

namespace Identity.Application.Interfaces.Persistance
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> GetAllAsync(bool trackChanges);
        void Create(T entity);  
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
        Task<T?> GetByIdAsync(Guid id, bool trackChanges);
    }
}
