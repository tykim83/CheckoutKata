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
        var items = new List<Item>()
        {
            new Item() {
                SKU = 'A',
                UnitPrice = 10.0m
            },
            new Item()
            {
                SKU = 'B',
                UnitPrice = 15.0m,
                Discount = new Discount() { UnitsRequired = 3, Value = 40, Type = DiscountType.Fixed }
            },
            new Item()
            {
                SKU = 'C',
                UnitPrice = 40.0m
            },
            new Item()
            {
                SKU = 'D',
                UnitPrice = 55.0m,
                Discount = new Discount() { UnitsRequired = 2, Value = 0.25m, Type = DiscountType.Percentage }
            }
        };

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

    [Fact]
    public void Scan_ShouldAddMultipleItemsToTheBasket_GivenValidItemsToAdd()
    {
        var itemsToScan = new char[] { 'A', 'A', 'C' };

        var result = _checkout.Scan(itemsToScan);

        result.Should().NotBeEmpty();
        result.Should().HaveCount(2);
        result.Should().ContainSingle(x => x.SKU == 'A' && x.Quantity == 2);
        result.Should().ContainSingle(x => x.SKU == 'C' && x.Quantity == 1);
    }

    [Fact]
    public void Scan_ShouldNotAddTheItemToTheBasket_GivenAnInvalidItemToAdd()
    {
        var itemsToScan = new char[] { 'F' };

        var result = _checkout.Scan(itemsToScan);

        result.Should().BeEmpty();
    }

    [Theory]
    [InlineData(new char[] { 'A', 'B' }, 25)]
    [InlineData(new char[] { 'A', 'B', 'C', 'D' }, 120)]
    [InlineData(new char[] { 'A', 'A', 'A' }, 30)]
    public void CalculateTotal_ShouldCalculateCorrectTotal_GivenMultipleItemsHaveBeenAddedToTheBasket(char[] itemsToScan, decimal total)
    {
        _checkout.Scan(itemsToScan);

        var result = _checkout.CalculateTotal();

        result.Should().Be(total);
    }

    [Theory]
    [InlineData(new char[] { 'B', 'B' }, 30)]
    [InlineData(new char[] { 'B', 'B', 'B' }, 40)]
    [InlineData(new char[] { 'B', 'B', 'B', 'B' }, 55)]
    [InlineData(new char[] { 'B', 'B', 'B', 'B', 'B', 'B' }, 80)]
    public void CalculateTotal_ShouldCalculateCorrectTotal_GivenMultipleItemsWithFixedDiscountHaveBeenAddedToTheBasket(char[] itemsToScan, decimal total)
    {
        _checkout.Scan(itemsToScan);

        var result = _checkout.CalculateTotal();

        result.Should().Be(total);
    }

    [Theory]
    [InlineData(new char[] { 'D' }, 55)]
    [InlineData(new char[] { 'D', 'D' }, 82.5)]
    [InlineData(new char[] { 'D', 'D', 'D' }, 137.5)]
    [InlineData(new char[] { 'D', 'D', 'D', 'D' }, 165)]
    public void CalculateTotal_ShouldCalculateCorrectTotal_GivenMultipleItemsWithPercentageDiscountHaveBeenAddedToTheBasket(char[] itemsToScan, decimal total)
    {
        _checkout.Scan(itemsToScan);

        var result = _checkout.CalculateTotal();

        result.Should().Be(total);
    }
}
