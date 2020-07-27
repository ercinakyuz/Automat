using System.Collections.Generic;

namespace Automat.Application.CommandHandlers.Common.Contracts
{
    public abstract class OrderContract
    {
        public IEnumerable<OrderItemContract> OrderItems { get; set; }
        public decimal Amount { get; set; }
    }
}