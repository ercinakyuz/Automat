using System.Collections.Generic;
using Automat.Application.CommandHandlers.Common.Contracts;
using MediatR;

namespace Automat.Application.CommandHandlers.CompleteOrderWithCash.Models
{
    public class CompleteOrderWithCashCommand : IRequest<CompleteOrderWithCashCommandResult>
    {
        public IEnumerable<BasketItemContract> BasketItems { get; set; }
        public decimal Amount { get; set; }
    }
}