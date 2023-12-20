using API.Services;

namespace API.Tests;

[Collection("Sequential")]
public class EditInboundOrderTest
{
    [Fact]
    public async void PassTestEditInboundOrder()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var orderService = new OrderService(context, mapper, new AuthService(null, context), new ItemService(context, mapper));

        var inboundOrderToCreate = await SharedTesting.GetRandomInboundOrderDto(context, mapper);

        var inboundOrderCreated = await orderService.CreateInboundOrder(inboundOrderToCreate);
        Assert.NotNull(inboundOrderCreated);

        var inboundOrderToEdit = await SharedTesting.GetRandomInboundOrderDto(context, mapper);
        inboundOrderToEdit.Id = inboundOrderCreated.Id;

        var inboundOrderEdited = await orderService.EditInboundOrder(inboundOrderToEdit);
        Assert.NotNull(inboundOrderEdited);

        Assert.Equal(inboundOrderToEdit.TotalPrice, inboundOrderEdited.TotalPrice);
        Assert.Equal(inboundOrderToEdit.Supplier.Id, inboundOrderEdited.Supplier.Id);
        Assert.Equal(inboundOrderToEdit.Supplier.Items, inboundOrderEdited.Supplier.Items);
        Assert.Equal(inboundOrderToEdit.Supplier.Name, inboundOrderEdited.Supplier.Name);
        Assert.Equal(inboundOrderToEdit.OrderDate, inboundOrderEdited.OrderDate);
        Assert.Equal(inboundOrderToEdit.OrderLines.Count, inboundOrderEdited.OrderLines.Count);
        await context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async void FailEditNonExistingInboundOrder()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var orderService = new OrderService(context, mapper, new AuthService(null, context), new ItemService(context, mapper));

        var inboundOrderToEdit = await SharedTesting.GetRandomInboundOrderDto(context, mapper);

        await Assert.ThrowsAsync<Exception>(async () => await orderService.EditInboundOrder(inboundOrderToEdit));
        await context.Database.EnsureDeletedAsync();
    }
}