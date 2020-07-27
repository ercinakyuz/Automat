using Automat.Infrastructure.Domain.Models;

namespace Automat.Domain.Payment.Models
{
    public abstract class PaymentOption : ValueObjectBase
    {
        protected PaymentOption(PaymentOptionDomainDto paymentOptionDomainDto)
        {
            
        }
    }

    public abstract class PaymentOptionDomainDto
    {
    }
}
