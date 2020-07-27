using System.Threading;
using System.Threading.Tasks;
using Automat.Domain.Order.Db;
using Automat.Domain.Order.Dtos;
using Automat.Domain.Order.Events;
using Automat.Domain.Order.Service.Requests;
using Automat.Domain.Order.Service.Responses;
using MediatR;

namespace Automat.Domain.Order.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediator _mediator;

        public OrderService(IOrderRepository orderRepository, IMediator mediator)
        {
            _orderRepository = orderRepository;
            _mediator = mediator;
        }

        public async Task<GetOrderResponse> GetOrderAsync(GetOrderRequest request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId);
            return new GetOrderResponse
            {
                Basket = order.Basket,
                Payment = order.Payment
            };
        }

        public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var order = Models.Order.Load(new OrderDomainDto
            {
                Basket = request.Basket,
                Payment = request.Payment
            });
            await _orderRepository.InsertAsync(order);
            await _mediator.Publish(new OrderCreated
            {
                OrderId = order.Id
            }, cancellationToken);

            return new CreateOrderResponse
            {
                Order = order
            };
        }
    }
}