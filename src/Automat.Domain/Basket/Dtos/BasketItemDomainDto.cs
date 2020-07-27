using Automat.Domain.Product.Dtos;

namespace Automat.Domain.Basket.Dtos
{
    public class BasketItemDomainDto
    {
        public int Quantity { get; set; }
        public ProductDomainDto Product { get; set; }
        public BasketItemDomainDto RelatedItem { get; set; }
    }
}