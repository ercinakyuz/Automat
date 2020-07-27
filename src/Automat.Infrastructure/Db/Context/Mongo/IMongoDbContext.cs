using MongoDB.Driver;

namespace Automat.Infrastructure.Db.Context.Mongo
{
    public interface IMongoDbContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>();
    }
}