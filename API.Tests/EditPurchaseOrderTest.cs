using API.DataTransferObjects;
using API.Enums;
using API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
namespace API.Tests;


[Collection("Sequential")]
public class EditPurchaseOrderTest
{
    /// <summary>
    /// Tests if EditPurchaseOrder() returns a correctly edited PurchaseOrder.
    /// </summary>
    [Fact]
    public async void PassEditPurchaseOrderTest()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var orderService = new OrderService(context, mapper, new AuthService(null, context), new ItemService(context, mapper));

        var firstPurchaseOrder = await SharedTesting.GetRandomPurchaseOrderDto(context, mapper);

        var createdPurchaseOrder = await orderService.CreatePurchaseOrder(firstPurchaseOrder);
        Assert.NotNull(createdPurchaseOrder);

        var editPurchaseOrder = await SharedTesting.GetRandomPurchaseOrderDto(context, mapper);
        editPurchaseOrder.Id = createdPurchaseOrder.Id;

        var editedPurchaseOrder = await orderService.EditPurchaseOrder(editPurchaseOrder);
        Assert.NotNull(editPurchaseOrder);

        Assert.Equal(editPurchaseOrder.Id,editedPurchaseOrder.Id);
        Assert.Equal(editPurchaseOrder.CustomerFirstName, editedPurchaseOrder.CustomerFirstName);
        Assert.Equal(editPurchaseOrder.CustomerLastName,editedPurchaseOrder.CustomerLastName);
        Assert.Equal(editPurchaseOrder.CustomerEmail,editedPurchaseOrder.CustomerEmail);
        Assert.Equal(editPurchaseOrder.City,editedPurchaseOrder.City);
        Assert.Equal(editPurchaseOrder.Country,editedPurchaseOrder.Country);
        Assert.Equal(editPurchaseOrder.PurchaseOrderState,editedPurchaseOrder.PurchaseOrderState);
        Assert.Equal(editPurchaseOrder.PostalCode,editedPurchaseOrder.PostalCode);
        Assert.Equal(editPurchaseOrder.CustomerPhone,editedPurchaseOrder.CustomerPhone);
        Assert.Equal(editPurchaseOrder.AddressLine,editedPurchaseOrder.AddressLine);
        Assert.Equal(editPurchaseOrder.TotalPrice,editedPurchaseOrder.TotalPrice);
        Assert.Equal(editPurchaseOrder.OrderLines.Count,editedPurchaseOrder.OrderLines.Count);

        await context.Database.EnsureDeletedAsync();
    }

    /// <summary>
    /// Tests if a non-existing purchase order can be edited.
    /// </summary>
    [Fact]
    public async void FailEditPurchaseOrderTest()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var orderService = new OrderService(context, mapper, new AuthService(null, context), new ItemService(context, mapper));

        var testPurchaseOrder = await SharedTesting.GetRandomPurchaseOrderDto(context, mapper);

        await Assert.ThrowsAsync<Exception>(async () =>await orderService.EditPurchaseOrder(testPurchaseOrder));
        await context.Database.EnsureDeletedAsync();
    }
}