using Automat.Domain.Payment.Dtos;

namespace Automat.Domain.Payment.Models
{
    public class CreditCardPaymentOption : PaymentOption
    {
        public CreditCardContactType ContactType { get; private set; }

        private CreditCardPaymentOption(CreditCardPaymentOptionDomainDto creditCardPaymentOptionDomainDto) : base(creditCardPaymentOptionDomainDto)
        {
            ContactType = creditCardPaymentOptionDomainDto.ContactType;
        }

        public static CreditCardPaymentOption Load(CreditCardPaymentOptionDomainDto creditCardPaymentOptionDomainDto)
        {
            return new CreditCardPaymentOption(creditCardPaymentOptionDomainDto);
        }
    }

}