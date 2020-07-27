 namespace Automat.Application.CommandHandlers.Common.Contracts
{
    public class BasketItemContract
    {
        public int Quantity { get; set; }
        public string Sku { get; set; }
        public BasketItemContract RelatedItem { get; set; }
    }
}