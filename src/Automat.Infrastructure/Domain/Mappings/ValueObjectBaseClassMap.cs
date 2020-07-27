using Automat.Infrastructure.Db.Models;
using Automat.Infrastructure.Domain.Models;
using MongoDB.Bson.Serialization;

namespace Automat.Infrastructure.Domain.Mappings
{
    public abstract class ValueObjectBaseClassMap<TEntity> : BsonClassMap<TEntity> where TEntity : ValueObjectBase
    {
        protected ValueObjectBaseClassMap()
        {
            AutoMap();
            SetIgnoreExtraElements(true);
        }
    }
}
