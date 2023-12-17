using API.DataTransferObjects;
using API.Enums;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumController : ControllerBase
    {
        private readonly IEnumService _enumService;

        public EnumController(IEnumService enumService)
        {
            _enumService = enumService;
        }

        [HttpPost]
        public async Task<ActionResult<CustomEnum>> PostCustomEnum([FromBody] CustomEnumDto customEnum)
        {
            try
            {
                return Ok(await _enumService.CreateCustomEnum(customEnum));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteCustomEnum(int id)
        {
            try
            {
                return Ok(await _enumService.DeleteCustomEnum(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomEnum>> GetCustomEnumById(int id)
        {
            try
            {
                return Ok(await _enumService.GetCustomEnumById(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Ok(false);
            }
        }

        [HttpGet("all/{enumType}")]
        public async Task<ActionResult<CustomEnum[]>> GetAllCustomEnumsOfType(EnumType enumType)
        {
            try
            {
                return Ok(await _enumService.GetCustomEnums(enumType));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(false);
            }
        }
    }
}
