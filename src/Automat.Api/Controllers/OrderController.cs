using System.Net;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Automat.Api.Models.Request;
using Automat.Api.Models.Response;
using Automat.Application.CommandHandlers.CompleteOrderWithCash;
using Automat.Application.CommandHandlers.CompleteOrderWithCash.Models;
using Automat.Application.CommandHandlers.CompleteOrderWithCreditCard;
using Automat.Application.CommandHandlers.CompleteOrderWithCreditCard.Models;
using Automat.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Automat.Api.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public OrderController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("api/order/cash")]
        [ProducesResponseType(typeof(CompleteOrderWithCashResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CompleteOrderWithCashResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CompleteOrderWithCash([FromBody]CompleteOrderWithCashRequest request, CancellationToken cancellationToken) =>
            await _mediator.SendStream<CompleteOrderWithCashCommand, CompleteOrderWithCashCommandResult>(new CompleteOrderWithCashCommand
            {
                BasketItems = request.BasketItems,
                Amount = request.Amount
            }, mapFrom => _mapper.Map<CompleteOrderWithCashCommandResult, CompleteOrderWithCashResponse>(mapFrom), cancellationToken);

        [HttpPost]
        [Route("api/order/creditcard")]
        [ProducesResponseType(typeof(CompleteOrderWithCreditCardResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(CompleteOrderWithCreditCardResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CompleteOrderWithCreditCard([FromBody]CompleteOrderWithCreditCardRequest request, CancellationToken cancellationToken) =>
            await _mediator.SendStream<CompleteOrderWithCreditCardCommand, CompleteOrderWithCreditCardCommandResult>(new CompleteOrderWithCreditCardCommand
            {
                BasketItems = request.BasketItems,
                Amount = request.Amount,
                CreditCardContactType = request.CreditCardContactType
            }, mapFrom => _mapper.Map<CompleteOrderWithCreditCardCommandResult, CompleteOrderWithCreditCardResponse>(mapFrom), cancellationToken);
    }
}