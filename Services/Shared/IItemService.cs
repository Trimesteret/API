using API.DataTransferObjects;
using API.Enums;
using API.Models.Items;

namespace API.Services.Shared;

public interface IItemService
{
    public Task<List<Item>> GetItemsBySearch(string search, int amountOfItemsShown, SortByPrice? sortByPrice, ItemType? itemType);

    public Task<Item> GetItemById(int id);

    public Task<int> GetItemCount(SortByPrice? sortByPrice, ItemType? itemType, string search = "");

    public Task<Item> CreateItem(ItemDto itemDto);

    public Task<Item> EditItem(ItemDto itemDto);
}
