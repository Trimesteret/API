using API.Services;

namespace API.Tests;

[Collection("Sequential")]
public class CreateInboundOrderTest
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
        await context.Database.EnsureDeletedAsync();
    }
}