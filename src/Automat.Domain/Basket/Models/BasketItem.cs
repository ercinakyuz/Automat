using Automat.Domain.Basket.Dtos;
using Automat.Infrastructure.Domain.Models;

namespace Automat.Domain.Basket.Models
{
    public class BasketItem : AggregateBase
    {
        private BasketItem(BasketItemDomainDto basketItem)
        {
            Quantity = basketItem.Quantity;
            Product = Domain.Product.Models.Product.Load(basketItem.Product);
            if (basketItem.RelatedItem != null)
            {
                RelatedItem = Load(basketItem.RelatedItem);
            }
        }

        public int Quantity { get; private set; }
        public Product.Models.Product Product { get; private set; }

        private decimal _price;
        public decimal Price
        {
            get => _price = CalculatePrice();
            private set => _price = value;
        }

        public BasketItem RelatedItem { get; private set; }

        public static BasketItem Load(BasketItemDomainDto basketItemDomainDto)
        {
            return new BasketItem(basketItemDomainDto);
        }

        public BasketItem SetRelatedItem(BasketItemDomainDto basketItemDomainDto)
        {
            RelatedItem = Load(basketItemDomainDto);
            return this;
        }

        private decimal CalculatePrice()
        {
            return Product.Price * Quantity + (RelatedItem?.Price * Quantity ?? 0);
        }

    }
}