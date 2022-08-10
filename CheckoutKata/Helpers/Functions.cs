namespace CheckoutKata.Helpers;

public static class Functions
{
    public static readonly Func<BasketItem, Item, decimal> CalculateFlatDiscount = (basketItem, item) =>
    {
        var discountMultiplier = basketItem.Quantity / item.Discount!.UnitsRequired;
        var fullPrice = item.UnitPrice * item.Discount.UnitsRequired;
        return (fullPrice - item.Discount.Value) * discountMultiplier;
    };

    public static readonly Func<BasketItem, Item, decimal> CalculatePercentageDiscount = (basketItem, item) =>
    {
        var discountMultiplier = basketItem.Quantity / item.Discount!.UnitsRequired;
        var fullPrice = item.UnitPrice * item.Discount.UnitsRequired;
        return (fullPrice * item.Discount.Value) * discountMultiplier;
    };
}
