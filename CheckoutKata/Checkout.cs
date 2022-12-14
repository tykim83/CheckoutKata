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
        decimal total = 0;

        foreach (var basketItem in _basket)
        {
            total += CalculateItemPrice(basketItem);
        }

        return total;
    }

    private decimal CalculateItemPrice(BasketItem basketItem)
    {
        var item = _itemsList.Single(c => c.SKU == basketItem.SKU);
        var discount = 0.0m;

        if (item.Discount is not null)
        {
            discount = item.Discount.Type switch
            {
                DiscountType.Fixed => Functions.CalculateFlatDiscount(basketItem, item),
                DiscountType.Percentage => Functions.CalculatePercentageDiscount(basketItem, item),
                _ => 0.0m,
            };
        }

        return basketItem.Quantity * item.UnitPrice - discount;
    }
}
