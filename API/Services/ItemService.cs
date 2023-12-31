using API.DataTransferObjects;
using API.Enums;
using API.Models;
using API.Models.Items;
using API.Models.Orders;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class ItemService : IItemService
{
    private readonly SharedContext _sharedContext;
    private readonly IMapper _mapper;

    public ItemService(SharedContext dbSharedContext, IMapper mapper)
    {
        _sharedContext = dbSharedContext;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets items by a search, sort and filter
    /// </summary>
    /// <param name="search"></param>
    /// <param name="amountOfItemsShown"></param>
    /// <param name="sortByPrice"></param>
    /// <param name="itemType"></param>
    /// <returns></returns>
    public async Task<List<Item>> GetItemsBySearch(string? search, int amountOfItemsShown, SortByPrice? sortByPrice,
        ItemType? itemType)
    {
        IQueryable<Item> query = _sharedContext.Items;

        query = AddSearchToQuery(sortByPrice, itemType, query, search);

        return await query.Take(amountOfItemsShown).ToListAsync();
    }

    /// <summary>
    /// Gets an item given the id of the item
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ItemDto> GetItemById(int id)
    {
        var item = await _sharedContext.Items.FirstOrDefaultAsync(item => item.Id == id);

        if (item == null)
        {
            throw new Exception("Item could not be found");
        }

        var itemDto = _mapper.Map<ItemDto>(item);

        var wine = await _sharedContext.Wines.Include(wine => wine.WineTypeEnum).FirstOrDefaultAsync(wine => wine.Id == id);
        if (wine != null)
        {
            itemDto.SuitableForEnumIds = await wine.GetSuitableForAsIntList(_sharedContext);
            itemDto.WineTypeEnum = wine.WineTypeEnum;
        }

        var liquor = await _sharedContext.Liquors.Include(liquor => liquor.LiquorTypeEnum).FirstOrDefaultAsync(liquor => liquor.Id == id);
        if (liquor != null)
        {
            itemDto.LiquorTypeEnum = liquor.LiquorTypeEnum;
        }

        return itemDto;
    }

    /// <summary>
    /// Creates a new item given an itemDto
    /// </summary>
    /// <param name="itemDto"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ItemDto> CreateItem(ItemDto itemDto)
    {
        switch (itemDto.ItemType)
        {
            case ItemType.Wine:
                Wine wine = new Wine(itemDto, _sharedContext);
                _sharedContext.Wines.Add(wine);
                await wine.SetSuitableFor(_sharedContext, itemDto.SuitableForEnumIds);
                await _sharedContext.SaveChangesAsync();
                var createdItemDto = _mapper.Map<ItemDto>(wine);
                createdItemDto.SuitableForEnumIds = await wine.GetSuitableForAsIntList(_sharedContext);
                return createdItemDto;

            case ItemType.Liquor:
                Liquor liquor = new Liquor(itemDto, _sharedContext);
                _sharedContext.Liquors.Add(liquor);
                await _sharedContext.SaveChangesAsync();
                return _mapper.Map<ItemDto>(liquor);

            case ItemType.DefaultItem:
                DefaultItem defaultItem = new DefaultItem(itemDto);
                _sharedContext.DefaultItems.Add(defaultItem);
                await _sharedContext.SaveChangesAsync();
                return _mapper.Map<ItemDto>(defaultItem);
            default:
                throw new NotImplementedException("Item not created");
        }
    }

    /// <summary>
    /// Get the amount of items that match the search, sort and filter
    /// </summary>
    /// <param name="sortByPrice"></param>
    /// <param name="itemType"></param>
    /// <param name="search"></param>
    /// <returns></returns>
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
    private IQueryable<Item> AddSearchToQuery(SortByPrice? sortByPrice, ItemType? itemType, IQueryable<Item> query,
        string? search)
    {
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(item =>
                item.Name.ToLower().Contains(search.ToLower()) || item.Price.ToString().Contains(search));
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
    public async Task<ItemDto> EditItem(ItemDto itemDto)
    {
        var itemToEdit = await _sharedContext.Items.FirstOrDefaultAsync(itemToEdit => itemToEdit.Id == itemDto.Id);

        if (itemToEdit == null)
        {
            throw new Exception("Could not find item with id: " + itemDto.Id);
        }

        switch (itemToEdit)
        {
            case Wine wine:
                wine.ChangeWineProperties(itemDto);
                await wine.SetSuitableFor(_sharedContext, itemDto.SuitableForEnumIds);
                break;

            case Liquor liquor:
                liquor.ChangeLiquorProperties(itemDto);
                break;

            case DefaultItem defaultItem:
                defaultItem.ChangeDefaultItemProperties(itemDto);
                break;

            default:
                throw new NotImplementedException("Item not edited");
        }

        await _sharedContext.SaveChangesAsync();

        var editedItemDto = _mapper.Map<ItemDto>(itemToEdit);
        if (itemToEdit is Wine edit)
        {
            editedItemDto.SuitableForEnumIds = await edit.GetSuitableForAsIntList(_sharedContext);
        }

        return editedItemDto;
    }

    /// <summary>
    /// Gets all items
    /// </summary>
    /// <returns></returns>
    public async Task<List<Item>> GetAllItems()
    {
        return await _sharedContext.Items.ToListAsync();
    }

    /// <summary>
    /// Gets all items related to a supplier
    /// </summary>
    /// <param name="supplierId">The id of the supplier</param>
    /// <returns></returns>
    /// <exception cref="Exception">If the supplier is not found</exception>
    public async Task<List<Item>> GetSupplierRelatedItems(int supplierId)
    {
        var supplier = await _sharedContext.Suppliers.FirstOrDefaultAsync(supplier => supplier.Id == supplierId);

        if (supplier == null)
        {
            throw new Exception("Supplier not found");
        }

        return await supplier.GetAssociatedItems(_sharedContext);
    }

    /// <summary>
    /// Delete an item given an id
    /// </summary>
    /// <param name="id">Id of the item to delete</param>
    /// <exception cref="Exception"></exception>
    public async Task DeleteItem(int id)
    {
        var existingItem = await _sharedContext.Items.FirstOrDefaultAsync(item => item.Id == id);

        if (existingItem == null)
        {
            throw new Exception("Item could not be found");
        }

        var existingWine = existingItem as Wine;

        if(existingWine != null)
        {
            await existingWine.ClearSuitableFor(_sharedContext);
        }

        _sharedContext.Remove(existingItem);

        await _sharedContext.SaveChangesAsync();
    }

    /// <summary>
    /// Reserves items given a list of orderLines
    /// </summary>
    /// <param name="orderLines"></param>
    /// <exception cref="Exception"></exception>
    public async Task ReserveItems(List<OrderLine> orderLines)
    {
        foreach (var orderLine in orderLines)
        {
            var item = await _sharedContext.Items.FirstOrDefaultAsync(item => item.Id == orderLine.ItemId);

            if (item == null)
            {
                throw new Exception("Item could not be found");
            }

            item.ReserveQuantity(orderLine.Quantity);
        }
    }
}
