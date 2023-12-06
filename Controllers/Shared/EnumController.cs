using API.Enums;
using API.Services.Shared;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Shared
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


        [HttpGet("{enumType}")]
        public async Task<ActionResult<CustomEnum[]>> GetSuitableForEnums(EnumType enumType)
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
