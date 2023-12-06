using API.DataTransferObjects;
using API.Enums;
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
        public async Task<ActionResult<List<Item>>> GetAllItems()
        {
            return await _itemService.GetAllItems();
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Item>>> GetItemsBySearch([FromQuery] string search, [FromQuery] int amountOfItemsShown, [FromQuery] SortByPrice? sortByPrice, [FromQuery] ItemType? itemType)
        {
            return await _itemService.GetItemsBySearch(search, amountOfItemsShown, sortByPrice, itemType);
        }


        [HttpGet("itemCount")]
        public async Task<ActionResult<int>> GetItemCount([FromQuery] string search, [FromQuery] SortByPrice? sortByPrice, [FromQuery] ItemType? itemType)
        {
            return await _itemService.GetItemCount(sortByPrice, itemType, search);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItemById(int id)
        {
            return await _itemService.GetItemById(id);
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostItem([FromBody] ItemDto item)
        {
            try
            {
                return await _itemService.CreateItem(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<ItemDto>> PutItem([FromBody] ItemDto item)
        {
            try
            {
                return await _itemService.EditItem(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }
    }
}
