using API.DataTransferObjects;
using API.Models.Orders;
using API.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize(Policy = "require-admin-role")]

        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            return await _orderService.GetAllOrders();
        }
    }
}
