using API.DataTransferObjects;
using API.Enums;
using API.Models.Items;

namespace API.Services;

public interface IItemService
{
    public Task<List<Item>> GetItemsBySearch(string? search, int amountOfItemsShown, SortByPrice? sortByPrice, ItemType? itemType);

    public Task<ItemDto> GetItemById(int id);

    public Task<int> GetItemCount(SortByPrice? sortByPrice, ItemType? itemType, string search = "");

    public Task<ItemDto> CreateItem(ItemDto itemDto);

    public Task<ItemDto> EditItem(ItemDto itemDto);

    public Task<List<Item>> GetAllItems();

    public Task<List<Item>> GetSupplierRelatedItems(int supplierId);

    public Task DeleteItem(int id);
}
