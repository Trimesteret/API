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
}