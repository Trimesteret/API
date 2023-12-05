using API.DataTransferObjects;
using API.Models;
using API.Models.Orders;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace API.Services.Shared;

public class OrderService : IOrderService
{
    private readonly SharedContext _sharedContext;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;

    public OrderService(SharedContext dbSharedContext, IAuthService authService, IMapper mapper)
    {
        _sharedContext = dbSharedContext;
        _authService = authService;
        _mapper = mapper;
    }
    public async Task<List<Order>> GetAllOrders()
    {
        var orders = await _sharedContext.Order.ToListAsync();
        return orders;
    }
}