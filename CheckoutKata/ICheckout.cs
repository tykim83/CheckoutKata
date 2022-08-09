namespace CheckoutKata;

public interface ICheckout
{
    IEnumerable<BasketItem> Scan(char[] itemsSku);
    decimal CalculateTotal();
}
