using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Automat.Application.CommandHandlers.Common.Contracts;
using Automat.Application.CommandHandlers.CompleteOrderWithCreditCard.Models;
using Automat.Domain.Basket.Models;
using Automat.Domain.Basket.Services;
using Automat.Domain.Order.Service;
using Automat.Domain.Order.Service.Requests;
using Automat.Domain.Payment.Dtos;
using Automat.Domain.Payment.Models;
using MediatR;

namespace Automat.Application.CommandHandlers.CompleteOrderWithCreditCard
{
    public class CompleteOrderWithCreditCardCommandHandler : IRequestHandler<CompleteOrderWithCreditCardCommand, CompleteOrderWithCreditCardCommandResult>
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public CompleteOrderWithCreditCardCommandHandler(IBasketService basketService, IOrderService orderService, IMapper mapper)
        {
            _basketService = basketService;
            _orderService = orderService;
            _mapper = mapper;
        }
        public async Task<CompleteOrderWithCreditCardCommandResult> Handle(CompleteOrderWithCreditCardCommand request, CancellationToken cancellationToken)
        {
            var addProductsToBasketResponse = await _basketService.AddProductsToBasketAsync(new AddProductsToBasketRequestDto
            {
                BasketItems = request.BasketItems.Select(basketItem => new BasketItemDto
                {
                    Sku = basketItem.Sku,
                    Quantity = basketItem.Quantity,
                    RelatedItem = basketItem.RelatedItem != null ? new BasketItemDto
                    {
                        Sku = basketItem.RelatedItem.Sku,
                        Quantity = basketItem.RelatedItem.Quantity
                    } : null
                })
            }, cancellationToken);

            if (addProductsToBasketResponse?.Basket != null && addProductsToBasketResponse.Basket.Price == request.Amount)
            {
                var payment = Payment.Load(new PaymentDomainDto
                {
                    Amount = request.Amount
                });
                payment.SetPaymentOptionAsCreditCard(new CreditCardPaymentOptionDomainDto
                {
                    ContactType = (CreditCardContactType)request.CreditCardContactType
                });
                var createOrderResponse = await _orderService.CreateOrderAsync(new CreateOrderRequest
                {
                    Basket = addProductsToBasketResponse.Basket,
                    Payment = payment
                }, cancellationToken);

                if (createOrderResponse.Order != null)
                {
                    return new CompleteOrderWithCreditCardCommandResult
                    {
                        Order = new OrderWithCreditCardContract
                        {
                            Amount = createOrderResponse.Order.Payment.Amount,
                            ContactType = (CreditCardContactTypeContract)((CreditCardPaymentOption)createOrderResponse.Order.Payment.PaymentOption).ContactType,
                            OrderItems = _mapper.Map<IEnumerable<BasketItem>, IEnumerable<OrderItemContract>>(createOrderResponse.Order.Basket.Items)
                        }
                    };
                }

            }

            return new CompleteOrderWithCreditCardCommandResult();
        }
    }
}