using Automat.Infrastructure.Common.Contracts;

namespace Automat.Application.CommandHandlers.CompleteOrderWithCash.Models
{
    public class CompleteOrderWithCashCommandResult: CommandResultBase
    {
        public OrderWithCashContract Order { get; set; }
    }
}