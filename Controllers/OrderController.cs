using API.DataTransferObjects;
using API.Models.Orders;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
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
        public async Task<ActionResult<PurchaseOrderDto>> PostPurchaseOrder([FromBody] PurchaseOrderDto purchaseOrder)
        {
            try
            {
                return Ok(await _orderService.CreatePurchaseOrder(purchaseOrder));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("inboundOrder/{id}")]
        public async Task<InboundOrderDto> GetInboundOrderById(int id)
        {
            return await _orderService.GetInboundOrderById(id);
        }

        [HttpPut("inboundOrder")]
        public async Task<ActionResult<InboundOrderDto>> PutInboundOrder([FromBody] InboundOrderDto inboundOrder)
        {
            try
            {
                return await _orderService.EditInboundOrder(inboundOrder);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("inboundOrder")]
        public async Task<ActionResult<InboundOrderDto>> PostInboundOrder([FromBody] InboundOrderDto inboundOrder)
        {
            try
            {
                return Ok(await _orderService.CreateInboundOrder(inboundOrder));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
