using API.DataTransferObjects;
using API.Models.Items;
using Microsoft.AspNetCore.Mvc;
using API.Models.Suppliers;
using API.Services.Shared;

namespace API.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }
        

        [HttpPost]
        public async Task<ActionResult<Supplier>> PostSupplier([FromBody] SupplierDto supplierDto)
        {
            Console.WriteLine((supplierDto));
            try { await _supplierService.CreateSupplier(supplierDto);
                return Ok(supplierDto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        
        // [HttpGet("Associated/{id}")]
        // public async Task<ActionResult<List<Item>>> GetAssociated(int id)
        // {
        //     return await _supplierService.GetAssociated(id);
        // }
        
        

        // POST: api/Supplier
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Supplier>> GetSupplierById(int id)
        {
            return await _supplierService.GetSupplierById(id);
        }
        
        [HttpGet("AllSuppliers")]
        public async Task<ActionResult<List<Supplier>>> GetAlleSuppliers()
        {
            return await _supplierService.GetAllSuppliers();
        }
    }
}
