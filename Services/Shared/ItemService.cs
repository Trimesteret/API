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

    public async Task<List<Item>> GetItemsBySearch(string search, int amountOfItemsShown, SortByPrice? sortByPrice, WineType? wineType)
    {
        IQueryable<Item> query = _sharedContext.Items;

        query = AddSearchToQuery(search, sortByPrice, wineType, query);

        return await query.Take(amountOfItemsShown).ToListAsync();
    }

    public async Task<Item> GetItemById(int id)
    {
        return await _sharedContext.Items.FirstOrDefaultAsync(i => i.Id == id);
    }
    
    public async Task<Item> PostItem(ItemDto itemDto)
    {
        Item itemEntity;
        switch (itemDto.Type)
        {
            case ItemType.Wine:
                itemEntity = new Wine(itemDto.Id, itemDto.ItemName, itemDto.Ean, itemDto.ItemQuantity, itemDto.ItemPrice, itemDto.ItemDescription,
                    itemDto.ExpirationDate);
                break;
            case ItemType.Liquor:
                itemEntity = new Liquor(itemDto.ItemName, itemDto.Ean, itemDto.ItemQuantity, itemDto.ItemPrice);
                break;
            case ItemType.DefaultItem:
                itemEntity = new DefaultItem(itemDto.ItemName, itemDto.Ean, itemDto.ItemQuantity, itemDto.ItemPrice);
                break;
            default:
                throw new Exception("Item not created");
        }
    
        await _sharedContext.Items.AddAsync(itemEntity);
    
        await _sharedContext.SaveChangesAsync();

        return itemEntity;
    }


    public Task<int> GetItemCount(string? search, SortByPrice? sortByPrice, WineType? wineType)
    {
        IQueryable<Item> query = _sharedContext.Items;

        query = AddSearchToQuery(search, sortByPrice, wineType, query);

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
    private IQueryable<Item> AddSearchToQuery(string? search, SortByPrice? sortByPrice, WineType? wineType, IQueryable<Item> query)
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

        switch (wineType)
        {
            case WineType.RedWine:
                query = query.Where(item => item.WineType == WineType.RedWine);
                break;
            case WineType.WhiteWine:
                query = query.Where(item => item.WineType == WineType.WhiteWine);
                break;
            case WineType.RoseWine:
                query = query.Where(item => item.WineType == WineType.RoseWine);
                break;
        }

        return query;
    }
}
