using System.Collections.Generic;
using System.Linq;
using Automat.Domain.Basket.Dtos;
using Automat.Infrastructure.Domain.Models;

namespace Automat.Domain.Basket.Models
{
    public class Basket : AggregateBase
    {
        private List<BasketItem> _items;
        public IEnumerable<BasketItem> Items
        {
            get => _items;
            private set => _items = value.ToList();
        }

        private decimal _price;
        public decimal Price
        {
            get => _price = CalculatePrice();
            private set => _price = value;
        }

        private Basket(BasketDomainDto basketDomainDto)
        {
            _items = new List<BasketItem>();
        }

        public static Basket Load(BasketDomainDto basketDomainDto)
        {
            return new Basket(basketDomainDto);
        }
        public void AddBasketItems(IEnumerable<BasketItem> basketItems)
        {
            _items.AddRange(basketItems);
        }
        public void AddBasketItem(BasketItem basketItem)
        {
            _items.Add(basketItem);
        }

        private decimal CalculatePrice()
        {
            return Items.Sum(basketItem => basketItem.Price);
        }
    }
}
