using Automat.Domain.Product.Dtos;
using Automat.Infrastructure.Domain.Models;

namespace Automat.Domain.Product.Models
{
    public class Product : AggregateBase
    {
        private Product(ProductDomainDto productDomainDto)
        {
            Name = productDomainDto.Name;
            Price = productDomainDto.Price;
            Sku = productDomainDto.Sku;
            AvailableQuantity = productDomainDto.AvailableQuantity;
            Category = Category.Load(productDomainDto.Category);
        }
        public string Name { get; private set; }
        public string Sku { get; private set; }
        public decimal Price { get; private set; }
        public int AvailableQuantity { get; private set; }
        public Category Category { get; private set; }


        public static Product Load(ProductDomainDto productDomainDto)
        {
            return new Product(productDomainDto);
        }

        public Product SetAvailableQuantity(int availableQuantity)
        {
            AvailableQuantity = availableQuantity;
            return this;
        }

    }
}