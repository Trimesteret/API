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
            var order = await _orderService.GetAllOrders();
            return await _orderService.GetAllOrders();
        }

        [HttpGet("purchaseOrder/{id}")]
        public async Task<PurchaseOrderDto> GetPurchaseOrderById(int id)
        {
            return await _orderService.GetPurchaseOrderById(id);
        }

        [HttpPut("purchaseOrder")]
        public async Task<PurchaseOrderDto> PutPurchaseOrder([FromBody] PurchaseOrderDto purchaseOrder)
        {
            return await _orderService.EditPurchaseOrder(purchaseOrder);
        }

        [HttpPost("purchaseOrder")]
        public async Task<PurchaseOrderDto> PostPurchaseOrder([FromBody] PurchaseOrderDto purchaseOrder)
        {
            return await _orderService.CreatePurchaseOrder(purchaseOrder);
        }

        [HttpGet("inboundOrder/{id}")]
        public async Task<InboundOrderDto> GetInboundOrderById(int id)
        {
            return await _orderService.GetInboundOrderById(id);
        }

        [HttpPut("inboundOrder")]
        public async Task<InboundOrderDto> PutInboundOrder([FromBody] InboundOrderDto inboundOrder)
        {
            return await _orderService.EditInboundOrder(inboundOrder);
        }

        [HttpPost("inboundOrder")]
        public async Task<InboundOrderDto> PostInboundOrder([FromBody] InboundOrderDto inboundOrder)
        {
            return await _orderService.CreateInboundOrder(inboundOrder);
        }
    }
}
