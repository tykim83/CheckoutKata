namespace CheckoutKata;

public class Checkout : ICheckout
{
    private readonly IEnumerable<Item> _itemsList;
    private ICollection<BasketItem> _basket;

    public Checkout(IEnumerable<Item> itemsList)
    {
        _itemsList = itemsList;
        _basket = new List<BasketItem>();
    }

    public IEnumerable<BasketItem> Scan(char[] itemsSku)
    {
        throw new NotImplementedException();
    }
}


