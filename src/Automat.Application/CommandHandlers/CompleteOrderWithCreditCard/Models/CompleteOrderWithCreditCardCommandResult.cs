using Automat.Infrastructure.Common.Contracts;

namespace Automat.Application.CommandHandlers.CompleteOrderWithCreditCard.Models
{
    public class CompleteOrderWithCreditCardCommandResult: CommandResultBase
    {
        public OrderWithCreditCardContract Order { get; set; }
    }
}