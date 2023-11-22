using API.Models.Items;
using API.Services.Shared;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetItemsBySearch([FromQuery] string search)
        {
            Console.WriteLine(search);
            return await _itemService.GetItemsBySearch(search);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemById(int id)
        {
            return await _itemService.GetItemById(id);
        }
    }
}
