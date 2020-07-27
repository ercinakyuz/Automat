namespace Automat.Application.CommandHandlers.Common.Contracts
{
    public class OrderItemContract
    {
        public OrderProductContract Product { get; set; }
        public OrderItemContract RelatedItem { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}