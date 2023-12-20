using API.DataTransferObjects;
using API.Enums;
using API.Services;
using Microsoft.AspNetCore.Http;
namespace API.Tests;

[Collection("Sequential")]
public class GetPurchaseOrderByIdTest
{
    /// <summary>
    /// Tests if a purchase order can be fetched back from the database with, and still have the same values.
    /// </summary>
    [Fact]
    public async void PassGetPurchaseOrderByIdTest()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();
        var httpProcessor = new HttpContextAccessor();
        var authService = new AuthService(httpProcessor, context);
        var itemService = new ItemService(context, mapper);
        var orderService = new OrderService(context, mapper, authService, itemService);

        var testPurchaseOrder = await SharedTesting.GetRandomPurchaseOrderDto(context, mapper);

        var createdPurchaseOrder = await orderService.CreatePurchaseOrder(testPurchaseOrder);
        Assert.NotNull(createdPurchaseOrder);

        var fetchedPurchaseOrder = await orderService.GetPurchaseOrderById(createdPurchaseOrder.Id ?? throw new Exception("Purchase order ID is null"));
        Assert.NotNull(fetchedPurchaseOrder);

        Assert.Equal(createdPurchaseOrder.CustomerFirstName, fetchedPurchaseOrder.CustomerFirstName);
        Assert.Equal(createdPurchaseOrder.CustomerLastName,fetchedPurchaseOrder.CustomerLastName);
        Assert.Equal(createdPurchaseOrder.CustomerEmail,fetchedPurchaseOrder.CustomerEmail);
        Assert.Equal(createdPurchaseOrder.City,fetchedPurchaseOrder.City);
        Assert.Equal(createdPurchaseOrder.Country,fetchedPurchaseOrder.Country);
        Assert.Equal(createdPurchaseOrder.PurchaseOrderState,fetchedPurchaseOrder.PurchaseOrderState);
        Assert.Equal(createdPurchaseOrder.PostalCode,fetchedPurchaseOrder.PostalCode);
        Assert.Equal(createdPurchaseOrder.CustomerPhone,fetchedPurchaseOrder.CustomerPhone);
        Assert.Equal(createdPurchaseOrder.AddressLine,fetchedPurchaseOrder.AddressLine);
        Assert.Equal(createdPurchaseOrder.TotalPrice,fetchedPurchaseOrder.TotalPrice);
        Assert.Equal(createdPurchaseOrder.OrderLines.Count,fetchedPurchaseOrder.OrderLines.Count);

        await context.Database.EnsureDeletedAsync();
    }

    /// <summary>
    /// Tests if an invalid purchase order can be fetched from the database.
    /// </summary>
    [Fact]
    public async void FailGetPurchaseOrderByIdTestOrderDoesNotExist()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();
        var httpProcessor = new HttpContextAccessor();
        var authService = new AuthService(httpProcessor, context);
        var itemService = new ItemService(context, mapper);
        var orderService = new OrderService(context, mapper, authService, itemService);

        await Assert.ThrowsAsync<Exception>(async () => await orderService.GetInboundOrderById(-1));
    }
}