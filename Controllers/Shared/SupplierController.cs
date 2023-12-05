using API.DataTransferObjects;
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


        // POST: api/Supplier
        [HttpPost]
        
        public async Task<ActionResult<Supplier>> PostSupplier([FromBody]SupplierDto supplier)
        {
            try {
                await _supplierService.CreateSupplier(supplier);
                return Ok(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("AllSuppliers")]
        public async Task<ActionResult<List<Supplier>>> GetAlleSuppliers()
        {
            return await _supplierService.GetAllSuppliers();
        }
    }
}
