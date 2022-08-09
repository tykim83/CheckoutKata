namespace CheckoutKata.Models;

public class Item
{
    public char SKU { get; init; }
    public decimal UnitPrice { get; init; }
    public Discount? Discount { get; init; }
}
