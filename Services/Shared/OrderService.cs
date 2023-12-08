using API.DataTransferObjects;
using API.Models;
using API.Models.Orders;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace API.Services.Shared;

public class OrderService : IOrderService
{
    private readonly SharedContext _sharedContext;

    public OrderService(SharedContext dbSharedContext)
    {
        _sharedContext = dbSharedContext;
    }
    public async Task<List<Order>> GetAllOrders()
    {
        var orders = await _sharedContext.Order.ToListAsync();
        return orders;
    }
}