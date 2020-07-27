using Automat.Domain.Payment.Models;

namespace Automat.Domain.Payment.Dtos
{
    public class CashPaymentOptionDomainDto : PaymentOptionDomainDto
    {
        public decimal Change { get; set; }
    }
}