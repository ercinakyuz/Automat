namespace Automat.Application.CommandHandlers.CompleteOrderWithCreditCard.Models
{
    public class CompleteOrderWithCreditCardApplicationErrorCodes
    {
        public const string ECOWCC001 = "BasketCouldNotCreatedWhenAddingBasketItems";
        public const string ECOWCC002 = "ProductsAreNotAvailable";
        public const string ECOWCC003 = "PaidAmountDoesNotEqualBasketPrice";
        public const string ECOWCC004 = "OrderCouldNotBeCreated";
    }
}
