namespace Automat.Domain.Product.Dtos
{
    public class ProductDomainDto
    {
        public string Name { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public int AvailableQuantity { get; set; }
        public CategoryDomainDto Category { get; set; }
    }
}