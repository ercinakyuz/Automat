using System;
using Automat.Infrastructure.Db.Models;

namespace Automat.Infrastructure.Domain.Models
{
    public abstract class AggregateBase : IEntity<Guid>
    {
        protected AggregateBase()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }

    }
}