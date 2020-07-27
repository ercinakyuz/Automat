using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Automat.Infrastructure.Db.Repository;

namespace Automat.Domain.Product.Db
{
    public interface IProductRepository : IRepository<Models.Product, Guid>
    {
        Task<IEnumerable<Models.Product>> GetProductsBySkuListAsync(IEnumerable<string> skuList, CancellationToken cancellationToken);
    }
}
