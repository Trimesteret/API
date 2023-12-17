using API.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using API.Models.Suppliers;
using API.Services;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
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
        [Authorize(Policy = "require-admin-role")]
        public async Task<ActionResult<SupplierDto>> PostSupplier([FromBody] SupplierDto supplierDto)
        {
            try
            {
                return await _supplierService.CreateSupplier(supplierDto);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Authorize(Policy = "require-admin-role")]
        public async Task<ActionResult<SupplierDto>> EditSupplier([FromBody] SupplierDto supplier)
        {
            try
            {
                return await _supplierService.EditSupplier(supplier);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "require-admin-role")]
        public async Task<ActionResult<SupplierDto>> GetSupplierById(int id)
        {
            try
            {
                return await _supplierService.GetSupplierById(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Authorize(Policy = "require-admin-role")]
        public async Task<ActionResult<List<Supplier>>> GetAllSuppliers()
        {
            return await _supplierService.GetAllSuppliers();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "require-admin-role")]
        public async Task<ActionResult<Boolean>> DeleteSupplier(int id)
        {
            try
            {
                await _supplierService.DeleteSupplier(id);
                return Ok(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

    }
}
