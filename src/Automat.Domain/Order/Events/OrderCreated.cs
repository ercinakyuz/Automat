using System;
using Automat.Infrastructure.Domain.Models;

namespace Automat.Domain.Order.Events
{
    public class OrderCreated : EventBase
    {
        public Guid OrderId { get; set; }
    }
}
