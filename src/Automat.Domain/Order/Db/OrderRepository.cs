using System;
using Automat.Infrastructure.Db.Context.Mongo;
using Automat.Infrastructure.Db.Repository;

namespace Automat.Domain.Order.Db
{
    public class OrderMongoRepository : MongoRepositoryBase<Models.Order, Guid>, IOrderRepository
    {
        public OrderMongoRepository(IMongoDbContext dbContext) : base(dbContext)
        {

        }

    }
}