using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Musicians.Application.Interfaces.Persistance;
using Musicians.Domain.Models;
using System.Linq.Expressions;

namespace Musicians.Infrastructure.Persistance.Repositories
{
    public abstract class BaseRepository<T> :
        IBaseRepository<T> where T : BaseEntity
    {
        protected readonly IMongoCollection<T> _collection;

        protected readonly FilterDefinitionBuilder<T> _filterBuilder = Builders<T>.Filter;
        protected readonly UpdateDefinitionBuilder<T> _updateBuilder = Builders<T>.Update;

        public BaseRepository(IMongoCollection<T> collection) 
        {
            _collection = collection;
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default, ReplaceOptions? replaceOptions = null)
        {
            var filter = _filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            await _collection.ReplaceOneAsync(filter, entity, replaceOptions, cancellationToken);
        }

        public async Task CreateAsync(T entity, CancellationToken cancellationToken = default, InsertOneOptions? options = null)
        {
            await _collection.InsertOneAsync(entity, options, cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default, DeleteOptions? deleteOptions = null)
        {
            var filter = _filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
            await _collection.DeleteOneAsync(filter, deleteOptions, cancellationToken);
        }

        public async Task<IEnumerable<T>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
        {
            var result = await GetByCondition(genre => ids.Contains(genre.Id)).ToListAsync(cancellationToken);

            return result;
        }

        public IMongoQueryable<T> GetAll()
        {
            return _collection.AsQueryable();
        }

        public IMongoQueryable<T> GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _collection.AsQueryable().Where(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await GetAll().ToListAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await GetByCondition(entity => entity.Id == id).SingleOrDefaultAsync(cancellationToken);
        }
    }
}
