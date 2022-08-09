using CheckoutKata.Models;
using FluentAssertions;
using System.Collections.Generic;
using Xunit;

namespace CheckoutKata.Tests;

public class CheckoutTests
{
    private ICheckout _checkout;

    public CheckoutTests()
    {
        var items = new List<Item>();

        _checkout = new Checkout(items);
    }

    [Fact]
    public void Scan_ShouldAddAnItemToTheBasket_GivenAValidItemToAdd()
    {
        var itemsToScan = new char[] { 'A' };

        var result = _checkout.Scan(itemsToScan);

        result.Should().NotBeEmpty();
        result.Should().HaveCount(1);
        result.Should().ContainSingle(x => x.SKU == 'A');
    }
}
