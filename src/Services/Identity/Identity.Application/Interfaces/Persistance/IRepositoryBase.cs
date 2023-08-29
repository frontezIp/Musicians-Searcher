using System.Linq.Expressions;

namespace Identity.Application.Interfaces.Persistance
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetAllAsync(bool trackChanges);
        IQueryable<T> GetByConditionAsync(Expression<Func<T, bool>> expression,
            bool trackChanges);
        void Create(T entity);  
        void Delete(T entity);
        Task SaveChangesAsync();
    }
}
