namespace Automat.Domain.Order.Service.Requests
{
    public class CreateOrderRequest
    {
        public Basket.Models.Basket Basket { get; set; }
        public Payment.Models.Payment Payment { get; set; }
    }
}