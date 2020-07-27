using System;
using Automat.Infrastructure.Db.Repository;

namespace Automat.Domain.Order.Db
{
    public interface IOrderRepository : IRepository<Models.Order, Guid>
    {
    }
}
