namespace CheckoutKata;

public interface ICheckout
{
    IEnumerable<BasketItem> Scan(char[] itemsSku);
}
