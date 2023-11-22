using API.Models.Items;

namespace API.Services.Shared;

public interface IItemService
{
    public Task<List<Item>> GetItemsBySearch(string search);
    public Task<Item> GetItemById(int id);
}