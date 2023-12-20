using API.DataTransferObjects;
using API.Enums;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Http;
namespace API.Tests;

[Collection("Sequential")]
public class CreatePurchaseOrderTest
{
    /// <summary>
    /// Tests if a purchase order can be created and returned with no exceptions and without being null.
    /// </summary>
    [Fact]
    public async void PassCreatePurchaseOrderTest()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var orderService = new OrderService(context, mapper, new AuthService(null, context), new ItemService(context, mapper));

        var testPurchaseOrder = await SharedTesting.GetRandomPurchaseOrderDto(context, mapper);

        var createdPurchaseOrder = await orderService.CreatePurchaseOrder(testPurchaseOrder);
        Assert.NotNull(createdPurchaseOrder);

        Assert.Equal(createdPurchaseOrder.TotalPrice, testPurchaseOrder.TotalPrice);
        Assert.Equal(createdPurchaseOrder.CustomerFirstName, testPurchaseOrder.CustomerFirstName);
        Assert.Equal(createdPurchaseOrder.CustomerLastName, testPurchaseOrder.CustomerLastName);
        Assert.Equal(createdPurchaseOrder.CustomerPhone, testPurchaseOrder.CustomerPhone);
        Assert.Equal(createdPurchaseOrder.CustomerEmail, testPurchaseOrder.CustomerEmail);
        Assert.Equal(createdPurchaseOrder.AddressLine, testPurchaseOrder.AddressLine);
        Assert.Equal(createdPurchaseOrder.PostalCode, testPurchaseOrder.PostalCode);
        Assert.Equal(createdPurchaseOrder.City, testPurchaseOrder.City);
        Assert.Equal(createdPurchaseOrder.Country, testPurchaseOrder.Country);
        Assert.Equal(createdPurchaseOrder.OrderLines.Count, testPurchaseOrder.OrderLines.Count);
        Assert.Equal(createdPurchaseOrder.PurchaseOrderState, testPurchaseOrder.PurchaseOrderState);
        await context.Database.EnsureDeletedAsync();
    }

    /// <summary>
    /// Tests if it's possible to create a purchase order that already exists.
    /// </summary>
    [Fact]
    public async void FailCreatePurchaseOrderTestOrderAlreadyExists()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var orderService = new OrderService(context, mapper, new AuthService(null, context), new ItemService(context, mapper));

        var testPurchaseOrder = await SharedTesting.GetRandomPurchaseOrderDto(context, mapper);

        var createdPurchaseOrder = await orderService.CreatePurchaseOrder(testPurchaseOrder);
        testPurchaseOrder.Id = createdPurchaseOrder.Id;
        Assert.NotNull(createdPurchaseOrder);
        await Assert.ThrowsAsync<Exception>(async () =>await orderService.CreatePurchaseOrder(testPurchaseOrder));
        await context.Database.EnsureDeletedAsync();
    }
}