namespace CheckoutKata.Models;

public class Discount
{
    public int UnitsRequired { get; init; }
    public decimal Value { get; init; }
    public DiscountType Type { get; init; }
}
