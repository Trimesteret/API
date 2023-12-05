using API.DataTransferObjects;
using API.Models.Orders;

namespace API.Services.Shared;

public interface IOrderService
{
    public Task<List<Order>> GetAllOrders();
}