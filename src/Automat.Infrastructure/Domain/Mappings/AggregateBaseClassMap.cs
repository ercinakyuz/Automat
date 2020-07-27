using Automat.Infrastructure.Db.Models;
using Automat.Infrastructure.Domain.Models;
using MongoDB.Bson.Serialization;

namespace Automat.Infrastructure.Domain.Mappings
{
    public abstract class AggregateBaseClassMap<TEntity> : BsonClassMap<TEntity> where TEntity : AggregateBase
    {
        protected AggregateBaseClassMap()
        {
            MapIdProperty(entity => entity.Id);
            AutoMap();
            SetIgnoreExtraElements(true);
        }
    }
}
