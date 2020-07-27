using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Automat.Api.Models.Request;
using Automat.Api.Models.Response;
using Automat.Application.CommandHandlers.CompleteOrderWithCash;
using Automat.Application.CommandHandlers.CompleteOrderWithCash.Models;
using Automat.Application.CommandHandlers.CompleteOrderWithCreditCard;
using Automat.Application.CommandHandlers.CompleteOrderWithCreditCard.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Automat.Api.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("api/order/cash")]
        [ProducesResponseType(typeof(CompleteOrderWithCashResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CompleteOrderWithCashResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CompleteOrderWithCash([FromBody]CompleteOrderWithCashRequest request, CancellationToken cancellationToken)
        {
            var completeOrderCommandResult = await _mediator.Send(new CompleteOrderWithCashCommand
            {
                BasketItems = request.BasketItems,
                Amount = request.Amount
            }, cancellationToken);
            return new JsonResult(new CompleteOrderWithCashResponse
            {
                Result = completeOrderCommandResult.Order
            });
        }

        [HttpPost]
        [Route("api/order/creditcard")]
        [ProducesResponseType(typeof(CompleteOrderWithCreditCardResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CompleteOrderWithCreditCardResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CompleteOrderWithCreditCard([FromBody]CompleteOrderWithCreditCardRequest request, CancellationToken cancellationToken)
        {
            var completeOrderCommandResult = await _mediator.Send(new CompleteOrderWithCreditCardCommand
            {
                BasketItems = request.BasketItems,
                Amount = request.Amount,
                CreditCardContactType = request.CreditCardContactType
            }, cancellationToken);
            return new JsonResult(new CompleteOrderWithCreditCardResponse
            {
                Result = completeOrderCommandResult.Order
            });
        }
    }
}