using Identity.Application.Interfaces.Persistance;
using Identity.Infrastructure.Persistance.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Identity.Infrastructure.Persistance.Repositories
{
    internal class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
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

        public IQueryable<T> GetAllAsync(bool trackChanges)
        {
            return !trackChanges ?
                Context.Set<T>()
                    .AsNoTracking() :
                Context.Set<T>();
        }

        public IQueryable<T> GetByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            return !trackChanges ? 
                Context.Set<T>()
                    .Where(expression)
                    .AsNoTracking() :
                Context.Set<T>()
                    .Where(expression);
        }
    }
}
