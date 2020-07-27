using Automat.Domain.Order.Dtos;
using Automat.Infrastructure.Domain.Models;

namespace Automat.Domain.Order.Models
{
    public class Order : AggregateBase
    {
        private Order(OrderDomainDto orderDomainDto)
        {
            Basket = orderDomainDto.Basket;
            Payment = orderDomainDto.Payment;
        }
        public Basket.Models.Basket Basket { get; private set; }
        public Payment.Models.Payment Payment { get; private set; }

        public static Order Load(OrderDomainDto orderDomainDto)
        {
            return new Order(orderDomainDto);
        }
    }
}
