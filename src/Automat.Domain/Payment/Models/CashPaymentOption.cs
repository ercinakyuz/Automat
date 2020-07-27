using Automat.Domain.Payment.Dtos;

namespace Automat.Domain.Payment.Models
{
    public class CashPaymentOption : PaymentOption
    {
        public decimal Change { get; private set; }
        private CashPaymentOption(CashPaymentOptionDomainDto cashPaymentOptionDomainDto) : base(cashPaymentOptionDomainDto)
        {
            Change = cashPaymentOptionDomainDto.Change;
        }

        public static CashPaymentOption Load(CashPaymentOptionDomainDto cashPaymentOptionDomainDto)
        {
            return new CashPaymentOption(cashPaymentOptionDomainDto);
        }
    }
}