using Automat.Domain.Basket;
using Automat.Domain.Order;
using Automat.Domain.Product;
using Automat.Infrastructure.Db;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Automat.Domain
{
    public static class DomainServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainComponents(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return serviceCollection
                            .AddMongoDb(configuration)
                            .AddBasketDomainComponents()
                            .AddProductDomainComponents()
                            .AddOrderDomainComponents();
        }
    }
}
