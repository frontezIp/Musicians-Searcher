using Chat.DataAccess.Contexts;
using Chat.DataAccess.Models;
using Chat.DataAccess.Repositories.Interfaces;
using Chat.DataAccess.Specifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Chat.DataAccess.Repositories.Implementations
{
    internal class RepositoryBase<T> : IRepositoryBase<T>
        where T : BaseEntity
    {
        protected ChatContext Context { get; init; }

        public RepositoryBase(ChatContext context)
        {
            Context = context;
        }

        public void Create(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public async Task AddManyAsync(IEnumerable<T> entities)
        {
            await Context.Set<T>().AddRangeAsync(entities);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await Context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public void DeleteMany(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }

        public async Task<IEnumerable<T>> GetBySpecificationAsync(ISpecification<T> specification, CancellationToken cancellationToken) 
        {
            return await ApplySpecification(specification).ToListAsync(cancellationToken);
        }

        public async Task<T?> GetSingleBySpecificationAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            return await ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<int> CountBySpecificationAsync(ISpecification<T> specification, CancellationToken cancellationToken)
        {
            return await ApplySpecification(specification).CountAsync();
        }

        public IQueryable<T> GetAll(bool trackChanges)
        {
            return !trackChanges ?
                 Context.Set<T>()
                    .AsNoTracking() :
                 Context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges, CancellationToken cancellationToken)
        {
            var query = GetAll(trackChanges);

            var entities = await query.ToListAsync();

            return entities;
        }

        public async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges, CancellationToken cancellationToken)
        {
            return await GetByCondition(entity => ids.ToList().Contains(entity.Id), trackChanges)
                .ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(Guid id, bool trackChanges, CancellationToken cancellationToken)
        {
            return await GetByCondition(entity => entity.Id == id, trackChanges).SingleOrDefaultAsync();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ?
                Context.Set<T>()
                    .Where(expression)
                    .AsNoTracking() :
                Context.Set<T>()
                    .Where(expression);
        }

        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(Context.Set<T>().AsQueryable(), spec);
        }
    }
}
