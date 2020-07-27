namespace Automat.Domain.Order.Dtos
{
    public class OrderDomainDto
    {
        public Basket.Models.Basket Basket { get; set; }
        public Payment.Models.Payment Payment { get; set; }
    }
}