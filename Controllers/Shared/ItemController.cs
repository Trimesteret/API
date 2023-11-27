using API.DataTransferObjects;
using API.Enums;
using API.Models.Items;
using API.Services.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task<ActionResult<List<Item>>> GetItemsBySearch([FromQuery] string search, [FromQuery] int amountOfItemsShown, [FromQuery] SortByPrice? sortByPrice, [FromQuery] WineType? wineType)
        {
            return await _itemService.GetItemsBySearch(search, amountOfItemsShown, sortByPrice, wineType);
        }


        [HttpGet("itemCount")]
        public async Task<ActionResult<int>> GetItemCount([FromQuery] string search, [FromQuery] SortByPrice? sortByPrice, [FromQuery] WineType? wineType)
        {
            return await _itemService.GetItemCount(search, sortByPrice, wineType);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemById(int id)
        {
            return await _itemService.GetItemById(id);
        }
        
        [HttpPost("Item")]
        public async Task<ActionResult<Item>> PostItem([FromQuery] int id, [FromQuery] string name, [FromQuery] string ean,
                                                                                       [FromQuery] string description, [FromQuery] double price, [FromQuery] int quantity, 
                                                                                       [FromQuery] int year, [FromQuery] int volume, [FromQuery] double alcohol, 
                                                                                       [FromQuery] string country, [FromQuery] string grapesort, [FromQuery] string suitables, 
                                                                                       [FromQuery] string imageUrl, [FromQuery] WineType type)
        {
            
            var newItem = new ItemDto()
            {
                Id = id,
                AlcoholPercentage = alcohol,
                Country = country,
                Ean = ean,
                ExpirationDate = null, // THIS SHOULD NOT BE NULL
                Grapesort = grapesort,
                ItemDescription = description,
                ItemName = name,
                ItemPrice = price,
                ItemQuantity = quantity,
                WineType = type,
                Suitables = suitables,
                Type = ItemType.Wine, // Hardcoded for now, currently the ItemDto on AnGUI does not have "type", its type = WineType.
                Volume = volume,
                Year = year,
                ImageUrl = imageUrl
            };
            
            
            try
            {
                var tItem = await _itemService.PostItem(newItem);
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
