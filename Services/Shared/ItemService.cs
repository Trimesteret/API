using API.DataTransferObjects;
using API.Enums;
using API.Models;
using API.Models.Items;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Shared;

public class ItemService : IItemService
{
    private readonly SharedContext _sharedContext;

    public ItemService(SharedContext dbSharedContext)
    {
        _sharedContext = dbSharedContext;
    }

    public async Task<List<Item>> GetItemsBySearch(string search, int amountOfItemsShown, SortByPrice? sortByPrice, ItemType? itemType)
    {
        IQueryable<Item> query = _sharedContext.Items;

        query = AddSearchToQuery(search, sortByPrice, itemType, query);

        return await query.Take(amountOfItemsShown).ToListAsync();
    }

    public async Task<Item> GetItemById(int id)
    {
        return await _sharedContext.Items.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Item> CreateItem(ItemDto itemDto)
    {
        switch (itemDto.ItemType)
        {
            case ItemType.Wine:
                Wine wine = new Wine(itemDto.ItemName, itemDto.Ean, itemDto.ItemQuantity, itemDto.Price, itemDto.ItemDescription, itemDto.ItemType, itemDto.WineType);
                await _sharedContext.Wines.AddAsync(wine);
                await _sharedContext.SaveChangesAsync();
                return wine;
            case ItemType.Liquor:
                Liquor liquor = new Liquor(itemDto.ItemName, itemDto.Ean, itemDto.ItemQuantity, itemDto.Price, itemDto.ItemDescription, itemDto.ItemType);
                await _sharedContext.Liquors.AddAsync(liquor);
                await _sharedContext.SaveChangesAsync();
                return liquor;
            case ItemType.DefaultItem:
                DefaultItem defaultItem = new DefaultItem(itemDto.ItemName, itemDto.Ean, itemDto.ItemQuantity, itemDto.Price, itemDto.ItemDescription, itemDto.ItemType);
                await _sharedContext.DefaultItems.AddAsync(defaultItem);
                await _sharedContext.SaveChangesAsync();
                return defaultItem;
            default:
                throw new NotImplementedException("Item not created");
        }
    }


    public Task<int> GetItemCount(string? search, SortByPrice? sortByPrice, ItemType? itemType)
    {
        IQueryable<Item> query = _sharedContext.Items;

        query = AddSearchToQuery(search, sortByPrice, itemType, query);

        return query.CountAsync();
    }

    /// <summary>
    /// Adds the search, sort and filter to the query
    /// </summary>
    /// <param name="search">The search</param>
    /// <param name="sortByPrice">Sorting</param>
    /// <param name="itemType">Item filter</param>
    /// <param name="query">The query to add search, sorting and filter to</param>
    /// <returns></returns>
    private IQueryable<Item> AddSearchToQuery(string? search, SortByPrice? sortByPrice, ItemType? itemType, IQueryable<Item> query)
    {

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(item => item.Name.ToLower().Contains(search.ToLower()) || item.Price.ToString().Contains(search));
        }

        switch (sortByPrice)
        {
            case SortByPrice.Ascending:
                query = query.OrderBy(item => item.Price);
                break;
            case SortByPrice.Descending:
                query = query.OrderByDescending(item => item.Price);
                break;
        }

        switch (itemType)
        {
            case ItemType.Wine:
                query = query.Where(item => item.ItemType == ItemType.Wine);
                break;
            case ItemType.Liquor:
                query = query.Where(item => item.ItemType == ItemType.Liquor);
                break;
            case ItemType.DefaultItem:
                query = query.Where(item => item.ItemType == ItemType.DefaultItem);
                break;
        }

        return query;
    }
}
