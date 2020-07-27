namespace Automat.Domain.Order.Service.Responses
{
    public class GetOrderResponse
    {
        public Basket.Models.Basket Basket { get; set; }
        public Payment.Models.Payment Payment { get; set; }
    }
}