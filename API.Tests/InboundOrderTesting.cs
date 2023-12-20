using API.Services;

namespace API.Tests;

[Collection("Sequential")]
public class InboundOrderTesting
{
    [Fact]
    public async void PassTestCreateInboundOrder()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var orderService = new OrderService(context, mapper, new AuthService(null, context), new ItemService(context, mapper));

        var inboundOrderToCreate = await SharedTesting.GetRandomInboundOrderDto(context, mapper);

        var inboundOrderCreated = await orderService.CreateInboundOrder(inboundOrderToCreate);
        Assert.NotNull(inboundOrderCreated);

        Assert.Equal(inboundOrderToCreate.TotalPrice, inboundOrderCreated.TotalPrice);
        Assert.Equal(inboundOrderToCreate.Supplier.Id, inboundOrderCreated.Supplier.Id);
        Assert.Equal(inboundOrderToCreate.Supplier.Items, inboundOrderCreated.Supplier.Items);
        Assert.Equal(inboundOrderToCreate.Supplier.Name, inboundOrderCreated.Supplier.Name);
        Assert.Equal(inboundOrderToCreate.OrderDate, inboundOrderCreated.OrderDate);
        Assert.Equal(inboundOrderToCreate.OrderLines.Count, inboundOrderCreated.OrderLines.Count);
        await context.Database.EnsureDeletedAsync();
    }

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
    public async void FailCreateDuplicateInboundOrder()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var orderService = new OrderService(context, mapper, new AuthService(null, context), new ItemService(context, mapper));

        var inboundOrderToCreate = await SharedTesting.GetRandomInboundOrderDto(context, mapper);

        var inboundOrderCreated = await orderService.CreateInboundOrder(inboundOrderToCreate);
        Assert.NotNull(inboundOrderCreated);

        var inboundOrderToCreateDuplicate = await SharedTesting.GetRandomInboundOrderDto(context, mapper);
        inboundOrderToCreateDuplicate.Id = inboundOrderCreated.Id;
        inboundOrderToCreateDuplicate.SupplierName = inboundOrderCreated.Supplier.Name;

        await Assert.ThrowsAsync<Exception>(async () => await orderService.CreateInboundOrder(inboundOrderToCreateDuplicate));
        await context.Database.EnsureDeletedAsync();
    }

    [Fact]
    public async void FailCreateInboundOrderWithUnknownSupplier()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        var orderService = new OrderService(context, mapper, new AuthService(null, context), new ItemService(context, mapper));

        var inboundOrderToCreate = await SharedTesting.GetRandomInboundOrderDto(context, mapper);
        inboundOrderToCreate.Supplier.Id = -1;
        inboundOrderToCreate.SupplierName = "Unknown supplier";

        await Assert.ThrowsAsync<Exception>(async () => await orderService.CreateInboundOrder(inboundOrderToCreate));
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