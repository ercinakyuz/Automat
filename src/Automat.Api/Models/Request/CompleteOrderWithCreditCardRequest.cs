using System.Collections.Generic;
using Automat.Application.CommandHandlers.Common.Contracts;
using Automat.Application.CommandHandlers.CompleteOrderWithCreditCard;
using Automat.Application.CommandHandlers.CompleteOrderWithCreditCard.Models;

namespace Automat.Api.Models.Request
{
    public class CompleteOrderWithCreditCardRequest
    {
        public IEnumerable<BasketItemContract> BasketItems { get; set; }
        public decimal Amount { get; set; }
        public CreditCardContactTypeContract CreditCardContactType { get; set; }
    }
}