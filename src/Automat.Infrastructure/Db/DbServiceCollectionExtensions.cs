using Automat.Infrastructure.Db.Context.Mongo;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;

namespace Automat.Infrastructure.Db
{
    public static class DbServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            ConventionRegistry.Register("EnumStringConvention", new ConventionPack
            {
                new EnumRepresentationConvention(BsonType.String)
            }, t => true);
            var mongoDbConfiguration = configuration.GetSection("Db:Mongo");
            return serviceCollection
                .Configure<MongoDbOptions>(mongoDbConfiguration)
                .AddSingleton<IMongoDbContext, MongoDbContext>();
        }

    }
}
