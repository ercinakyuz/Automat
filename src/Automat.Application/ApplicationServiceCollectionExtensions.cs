using System;
using Automat.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Automat.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationComponents(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return serviceCollection
                .AddDomainComponents(configuration);
        }
    }
}
