namespace Automat.Domain.Product.Services.Requests
{
    public class DecreaseItemDto
    {
        public string Sku { get; set; }
        public int Quantity { get; set; }
    }
}