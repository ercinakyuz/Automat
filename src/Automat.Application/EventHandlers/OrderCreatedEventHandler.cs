using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Automat.Domain.Order.Events;
using Automat.Domain.Order.Service;
using Automat.Domain.Order.Service.Requests;
using Automat.Domain.Product.Services;
using Automat.Domain.Product.Services.Requests;
using MediatR;

namespace Automat.Application.EventHandlers
{
    public class OrderCreatedEventHandler : INotificationHandler<OrderCreated>
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;

        public OrderCreatedEventHandler(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }
        public async Task Handle(OrderCreated notification, CancellationToken cancellationToken)
        {
            var getOrderResponse = await _orderService.GetOrderAsync(new GetOrderRequest
            {
                OrderId = notification.OrderId
            }, cancellationToken);

            if (getOrderResponse?.Basket?.Items != null)
            {
                var itemsWillDecrease = new List<DecreaseItemDto>();

                foreach (var item in getOrderResponse.Basket.Items)
                {
                    itemsWillDecrease.Add(new DecreaseItemDto
                    {
                        Sku = item.Product.Sku,
                        Quantity = item.Quantity
                    });
                    if (item.RelatedItem != null)
                    {
                        itemsWillDecrease.Add(new DecreaseItemDto
                        {
                            Sku = item.RelatedItem.Product.Sku,
                            Quantity = item.RelatedItem.Quantity
                        });
                    }
                }

                if (itemsWillDecrease.Any())
                {
                    await _productService.DecreaseStockAsync(new DecreaseStockRequestDto
                    {
                        DecreaseItems = itemsWillDecrease
                    }, cancellationToken);
                }

            }
        }
    }
}
