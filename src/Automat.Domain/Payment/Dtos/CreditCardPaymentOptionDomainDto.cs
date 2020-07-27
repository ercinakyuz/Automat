using Automat.Domain.Payment.Models;

namespace Automat.Domain.Payment.Dtos
{
    public class CreditCardPaymentOptionDomainDto : PaymentOptionDomainDto
    {
        public CreditCardContactType ContactType { get; set; }
    }
}