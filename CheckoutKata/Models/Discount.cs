namespace CheckoutKata.Models;

public class Discount
{
    public int UnitRequired { get; init; }
    public decimal Value { get; init; }
    public DiscountType DiscountType { get; init; }
}
