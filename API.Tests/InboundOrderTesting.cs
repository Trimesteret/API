using API.Services;

namespace API.Tests;

public class InboundOrderTesting
{
    public async void PassTestCreateInboundOrder()
    {
        var context = SharedTesting.GetContext();
        var mapper = SharedTesting.GetMapper();

        //var orderService = new OrderService(context, mapper, new AuthService(context), new ItemService(context, mapper));
    }
}