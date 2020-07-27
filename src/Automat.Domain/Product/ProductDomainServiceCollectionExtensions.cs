using Automat.Domain.Product.Db;
using Automat.Domain.Product.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Automat.Domain.Product
{
    public static class ProductDomainServiceCollectionExtensions
    {
        public static IServiceCollection AddProductDomainComponents(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<IProductRepository, ProductMongoRepository>()
                .AddSingleton<IProductService, ProductService>();
        }
    }
}
