using Identity.Application.Interfaces.Persistance;
using Identity.Domain.Models;
using Identity.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Identity.Infrastructure.Persistance.Repositories
{
    internal class RepositoryBase<T> : IRepositoryBase<T>
        where T : BaseEntity
    {
        protected IdentityContext Context { get; init; }

        public RepositoryBase(IdentityContext context)
        {
            Context = context;
        }

        public void Create(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public IQueryable<T> GetAll(bool trackChanges)
        {
            return !trackChanges ?
                 Context.Set<T>()
                    .AsNoTracking() :
                 Context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(bool trackChanges)
        {
            var query = GetAll(trackChanges);

            var entities = await query.ToListAsync();

            return entities;
        }

        public async Task<T?> GetByIdAsync(Guid id, bool trackChanges)
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
    }
}
