using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Automat.Infrastructure.Db.Context.Mongo;
using Automat.Infrastructure.Db.Models;
using MongoDB.Driver;

namespace Automat.Infrastructure.Db.Repository
{
    public abstract class MongoRepositoryBase<TEntity, TId> : IRepository<TEntity, TId> where TEntity : IEntity<TId>
    {
        protected readonly IMongoCollection<TEntity> Collection;
        protected MongoRepositoryBase(IMongoDbContext dbContext)
        {
            Collection = dbContext.GetCollection<TEntity>();
        }
        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            return (await Collection.FindAsync(predicate)).ToEnumerable();
        }

        public virtual async Task<TEntity> GetByIdAsync(TId id)
        {
            return await (await Collection.FindAsync(entity => entity.Id.Equals(id))).FirstOrDefaultAsync();
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
        }
        public virtual async Task InsertManyAsync(IEnumerable<TEntity> entities)
        {
            await Collection.InsertManyAsync(entities);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            await Collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity);
        }

        public virtual async Task DeleteManyAsync(Expression<Func<TEntity, bool>> filter)
        {
            await Collection.DeleteManyAsync(filter);
        }


        public virtual async Task DeleteAsync(TId id)
        {
            await Collection.DeleteOneAsync(entity => entity.Id.Equals(id));
        }
    }
}