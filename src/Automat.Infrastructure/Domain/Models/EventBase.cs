using System;
using MediatR;

namespace Automat.Infrastructure.Domain.Models
{
    public abstract class EventBase : INotification
    {
        public Guid Id => Guid.NewGuid();
        public DateTime OccuredAt => DateTime.Now;
    }
}
