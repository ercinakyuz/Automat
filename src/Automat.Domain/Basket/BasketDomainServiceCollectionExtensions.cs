using Automat.Domain.Basket.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Automat.Domain.Basket
{
    public static class BasketDomainServiceCollectionExtensions
    {
        public static IServiceCollection AddBasketDomainComponents(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<IBasketService, BasketService>();

        }
    }
}
