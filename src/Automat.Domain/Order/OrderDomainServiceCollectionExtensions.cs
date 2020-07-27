using Automat.Domain.Order.Db;
using Automat.Domain.Order.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Automat.Domain.Order
{
    public static class OrderDomainServiceCollectionExtensions
    {
        public static IServiceCollection AddOrderDomainComponents(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<IOrderRepository, OrderMongoRepository>()
                .AddSingleton<IOrderService, OrderService>();
        }
    }
}
