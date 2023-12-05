using API.DataTransferObjects;
using API.Enums;
using API.Models;
using API.Models.Authentication;
using API.Models.Items;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Shared;

public class ItemService : IItemService
{
    private readonly SharedContext _sharedContext;
    private readonly IAuthService _authService;

    public ItemService(SharedContext dbSharedContext, IAuthService authService)
    {
        _sharedContext = dbSharedContext;
        _authService = authService;
    }

    public async Task<List<Item>> GetItemsBySearch(string search, int amountOfItemsShown, SortByPrice? sortByPrice, ItemType? itemType)
    {
        IQueryable<Item> query = _sharedContext.Items;

        query = AddSearchToQuery(sortByPrice, itemType, query, search);

        return await query.Take(amountOfItemsShown).ToListAsync();
    }

    public async Task<Item> GetItemById(int id)
    {
        return await _sharedContext.Items.FirstOrDefaultAsync(i => i.Id == id);
    }

    /// <summary>
    /// Creates a new item given an itemDto
    /// </summary>
    /// <param name="itemDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Item> CreateItem(ItemDto itemDto)
    {
        var activeUser = await _authService.GetActiveUser();

        if (activeUser.Role < Role.Admin)
        {
            throw new Exception("You do not have permission to create items");
        }

        if(activeUser is not Admin activeAdminUser) {
            throw new Exception("You do not have permission to create items");
        }

        switch (itemDto.ItemType)
        {
            case ItemType.Wine:
                var suitableFor = await _sharedContext.CustomEnums.Where(cEnum => itemDto.SuitableForEnumIds.Contains(cEnum.Id)).ToListAsync();
                Wine wine = activeAdminUser.CreateWine(itemDto, suitableFor);
                await _sharedContext.Wines.AddAsync(wine);
                await _sharedContext.SaveChangesAsync();
                return wine;
            case ItemType.Liquor:
                Liquor liquor = activeAdminUser.CreateLiquor(itemDto);
                await _sharedContext.Liquors.AddAsync(liquor);
                await _sharedContext.SaveChangesAsync();
                return liquor;
            case ItemType.DefaultItem:
                DefaultItem defaultItem = activeAdminUser.CreateDefaultItem(itemDto);
                await _sharedContext.DefaultItems.AddAsync(defaultItem);
                await _sharedContext.SaveChangesAsync();
                return defaultItem;
            default:
                throw new NotImplementedException("Item not created");
        }
    }


    public Task<int> GetItemCount(SortByPrice? sortByPrice, ItemType? itemType, string search = "")
    {
        IQueryable<Item> query = _sharedContext.Items;

        query = AddSearchToQuery(sortByPrice, itemType, query, search);

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
    private IQueryable<Item> AddSearchToQuery(SortByPrice? sortByPrice, ItemType? itemType, IQueryable<Item> query, string search = "")
    {
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(item => item.Name.ToLower().Contains(search.ToLower()) || item.Price.ToString().Contains(search));
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

        switch (sortByPrice)
        {
            case SortByPrice.Ascending:
                query = query.OrderBy(item => item.Price);
                break;
            case SortByPrice.Descending:
                query = query.OrderByDescending(item => item.Price);
                break;
        }

        return query;
    }

    /// <summary>
    /// Edits an item given the properties in the itemDto
    /// </summary>
    /// <param name="itemDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Item> EditItem(ItemDto itemDto)
    {
        var itemToEdit = await _sharedContext.Items.FirstOrDefaultAsync(itemToEdit => itemToEdit.Id == itemDto.Id);

        if (itemToEdit == null)
        {
            throw new Exception("Could not find item with id: " + itemDto.Id);
        }

        var activeUser = await _authService.GetActiveUser();

        if (activeUser.Role < Role.Admin)
        {
            throw new Exception("You do not have permission to create items");
        }

        if(activeUser is not Admin activeAdminUser) {
            throw new Exception("You do not have permission to create items");
        }

        switch (itemDto.ItemType)
        {
            case ItemType.Wine:
                Wine wine = (Wine)itemToEdit;
                var suitableFor = await _sharedContext.CustomEnums.Where(cEnum => itemDto.SuitableForEnumIds.Contains(cEnum.Id)).ToListAsync();
                wine.ChangeWineProperties(itemDto.ItemName, itemDto.Ean, itemDto.ItemQuantity, itemDto.Price,
                    itemDto.ItemDescription, itemDto.WineType, itemDto.Year, itemDto.Volume, itemDto.AlcoholPercentage,
                    itemDto.Country, itemDto.Region, itemDto.GrapeSort, itemDto.Winery, itemDto.TastingNotes,
                    suitableFor);
                await _sharedContext.SaveChangesAsync();
                return wine;
            case ItemType.Liquor:
                Liquor liquor = (Liquor)itemToEdit;
                liquor.ChangeLiquorProperties(itemDto.ItemName, itemDto.Ean, itemDto.ItemQuantity, itemDto.Price, itemDto.ItemDescription);
                await _sharedContext.SaveChangesAsync();
                return liquor;
            case ItemType.DefaultItem:
                DefaultItem defaultItem = (DefaultItem)itemToEdit;
                defaultItem.ChangeDefaultItemProperties(itemDto.ItemName, itemDto.Ean, itemDto.ItemQuantity, itemDto.Price, itemDto.ItemDescription);
                await _sharedContext.SaveChangesAsync();
                return defaultItem;
            default:
                throw new NotImplementedException("Item not edited");
        }
    }

    public async Task<List<Item>> GetAllItems()
    {
        return await _sharedContext.Items.ToListAsync();
    }
}
