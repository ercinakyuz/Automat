using Automat.Application.CommandHandlers.Common.Contracts;

namespace Automat.Application.CommandHandlers.CompleteOrderWithCash.Models
{
    public class OrderWithCashContract : OrderContract
    {
        public decimal Change { get; set; }
    }
}