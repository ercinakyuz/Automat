using Automat.Application.CommandHandlers.Common.Contracts;

namespace Automat.Application.CommandHandlers.CompleteOrderWithCreditCard.Models
{
    public class OrderWithCreditCardContract : OrderContract
    {
        public CreditCardContactTypeContract ContactType { get; set; }
    }
}