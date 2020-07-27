using System.Collections.Generic;
using Automat.Application.CommandHandlers.Common.Contracts;

namespace Automat.Api.Models.Request
{
    public class CompleteOrderWithCashRequest
    {
        public IEnumerable<BasketItemContract> BasketItems { get; set; }
        public decimal Amount { get; set; }
    }
}