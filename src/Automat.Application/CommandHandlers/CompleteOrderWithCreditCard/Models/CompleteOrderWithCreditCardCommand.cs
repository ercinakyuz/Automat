using System.Collections.Generic;
using Automat.Application.CommandHandlers.Common.Contracts;
using MediatR;

namespace Automat.Application.CommandHandlers.CompleteOrderWithCreditCard.Models
{
    public class CompleteOrderWithCreditCardCommand : IRequest<CompleteOrderWithCreditCardCommandResult>
    {
        public IEnumerable<BasketItemContract> BasketItems { get; set; }
        public decimal Amount { get; set; }
        public CreditCardContactTypeContract CreditCardContactType { get; set; }
    }
}