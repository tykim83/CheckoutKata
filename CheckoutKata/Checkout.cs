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
        foreach (var sku in itemsSku)
        {
            if (!_itemsList.Any(c => c.SKU == sku))
                continue;

            if (_basket.Any(c => c.SKU == sku))
            {
                _basket.Single(c => c.SKU == sku).Quantity++;
                continue;
            }

            _basket.Add(new BasketItem()
            {
                SKU = sku,
                Quantity = 1
            });
        }

        return _basket;
    }

    public decimal CalculateTotal()
    {
        throw new NotImplementedException();
    }
}
