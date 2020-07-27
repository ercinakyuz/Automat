using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Automat.Infrastructure.Db.Context.Mongo
{
    public class MongoDbContext: IMongoDbContext
    {
        private static IMongoClient _mongoClient;
        private readonly string _database;

        public MongoDbContext(IOptions<MongoDbOptions> mongoDbOptions)
        {
            _mongoClient = new MongoClient(mongoDbOptions.Value.ConnectionString);
            _database = mongoDbOptions.Value.Database;
        }
        public IMongoCollection<TEntity> GetCollection<TEntity>() => _mongoClient.GetDatabase(_database).GetCollection<TEntity>($"{typeof(TEntity).Name}s");
    }
}