using Chat.DataAccess.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DataAccess.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(Guid id, bool trackChanges, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetBySpecificationAsync(ISpecification<T> specification, CancellationToken cancellationToken);
        Task<int> CountBySpecificationAsync(ISpecification<T> specification, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges, CancellationToken cancellationToken);
        Task AddManyAsync(IEnumerable<T> entities);
        void DeleteMany(IEnumerable<T> entities);
        Task<T?> GetSingleBySpecificationAsync(ISpecification<T> specification, CancellationToken cancellationToken);
    }
}
