namespace Automat.Application.CommandHandlers.CompleteOrderWithCash.Models
{
    public class CompleteOrderWithCashApplicationErrorCodes
    {
        public const string ECOWC001 = "BasketCouldNotCreatedWhenAddingBasketItems";
        public const string ECOWC002 = "ProductsAreNotAvailable";
        public const string ECOWC003 = "PaidAmountLesserThanBasketPrice";
        public const string ECOWC004 = "OrderCouldNotBeCreated";
    }
}
