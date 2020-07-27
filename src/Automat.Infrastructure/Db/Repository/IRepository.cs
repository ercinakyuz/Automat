using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Automat.Infrastructure.Db.Models;

namespace Automat.Infrastructure.Db.Repository
{
    public interface IRepository<TEntity, in TId> where TEntity : IEntity<TId>
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);
        Task<TEntity> GetByIdAsync(TId id);
        Task InsertAsync(TEntity entity);
        Task InsertManyAsync(IEnumerable<TEntity> entities);
        Task UpdateAsync(TEntity entity);
        Task DeleteManyAsync(Expression<Func<TEntity, bool>> filter);
        Task DeleteAsync(TId id);
    }
}
