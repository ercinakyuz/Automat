using Automat.Domain.Payment.Dtos;
using Automat.Infrastructure.Domain.Models;

namespace Automat.Domain.Payment.Models
{
    public class Payment : AggregateBase
    {
        public decimal Amount { get; private set; }
        public PaymentOption PaymentOption { get; private set; }
        private Payment(PaymentDomainDto paymentDomainDto)
        {
            Amount = paymentDomainDto.Amount;
        }

        public static Payment Load(PaymentDomainDto paymentDomainDto)
        {
            return new Payment(paymentDomainDto);
        }

        public CreditCardPaymentOption SetPaymentOptionAsCreditCard(CreditCardPaymentOptionDomainDto creditCardPaymentOptionDomainDto)
        {
            var paymentOption = CreditCardPaymentOption.Load(creditCardPaymentOptionDomainDto);
            PaymentOption = paymentOption;
            return paymentOption;
        }
        public CashPaymentOption SetPaymentOptionAsCash(CashPaymentOptionDomainDto cashPaymentOptionDomainDto)
        {
            var paymentOption = CashPaymentOption.Load(cashPaymentOptionDomainDto);
            PaymentOption = paymentOption;
            return paymentOption;
        }
    }
}
