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

        [HttpGet("PurchaseOrders")]
        [Authorize(Policy = "require-admin-role")]
        public async Task<ActionResult<List<PurchaseOrder>>> GetPurchaseOrders()
        {
            return await _orderService.GetAllPurchaseOrders();
        }

        [HttpGet("currentUserPurchaseOrders")]
        [Authorize(Policy = "require-admin-role")]
        public async Task<ActionResult<List<PurchaseOrder>>> GetCurentUserPurchaseOrders()
        {
            return await _orderService.GetCurrentUserPurchaseOrders();
        }

        [HttpGet("InboundOrders")]
        [Authorize(Policy = "require-admin-role")]
        public async Task<ActionResult<List<InboundOrder>>> GetInboundOrders()
        {
            return await _orderService.GetAllInboundOrders();
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
        public async Task<ActionResult<InboundOrderDto>> GetInboundOrderById(int id)
        {
            try
            {
                return Ok(await _orderService.GetInboundOrderById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
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
