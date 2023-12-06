using API.Models.Suppliers;
using API.DataTransferObjects;
using API.Enums;
using API.Models;
using API.Models.Items;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;


namespace API.Services.Shared;

public class SupplierService : ISupplierService
{
    private readonly SharedContext _sharedContext;
    private readonly ItemService _itemService;
    
    public SupplierService(SharedContext dbSharedContext)
    {
        _sharedContext = dbSharedContext;
        
    }
    
    public async Task<Supplier> CreateSupplier(SupplierDto supplierDto)
    {
        
        Supplier supplier = new Supplier(supplierDto.Name);
        
        await _sharedContext.Suppliers.AddAsync(supplier);
        await _sharedContext.SaveChangesAsync();
        
        
        if(supplierDto.Items != null)
        {
            foreach (var itemDto in supplierDto.Items)
            {

                await EditItemList(itemDto);
                // await _itemService.EditItem(item);
                // await _sharedContext.SaveChangesAsync();
            }
        }
        
        return supplier;

    }

    public async Task<Item> EditItemList(ItemDto itemDto)
    {
        var itemToEdit = await _sharedContext.Items.FirstOrDefaultAsync(itemToEdit => itemToEdit.Id == itemDto.Id);
        switch (itemDto.ItemType)
        {

            case ItemType.Wine:
                Wine wine = (Wine)itemToEdit;
                wine.ChangeWineProperties(itemDto.ItemName, itemDto.SupplierId, itemDto.Ean, itemDto.ItemQuantity, itemDto.Price,
                    itemDto.ItemDescription, itemDto.ItemType, itemDto.WineType, itemDto.Year, itemDto.Volume,
                    itemDto.AlcoholPercentage, itemDto.Country, itemDto.Region, itemDto.GrapeSort, itemDto.Winery,
                    itemDto.TastingNotes, itemDto.SuitableFor);
                await _sharedContext.SaveChangesAsync();
                return wine;
            case ItemType.Liquor:
                Liquor liquor = (Liquor)itemToEdit;
                liquor.ChangeLiquorProperties(itemDto.ItemName, itemDto.SupplierId, itemDto.Ean, itemDto.ItemQuantity, itemDto.Price,
                    itemDto.ItemDescription, itemDto.ItemType);
                await _sharedContext.SaveChangesAsync();
                return liquor;
            case ItemType.DefaultItem:
                DefaultItem defaultItem = (DefaultItem)itemToEdit;
                defaultItem.ChangeDefaultItemProperties(itemDto.ItemName, itemDto.SupplierId, itemDto.Ean, itemDto.ItemQuantity,
                    itemDto.Price, itemDto.ItemDescription, itemDto.ItemType);
                await _sharedContext.SaveChangesAsync();
                return defaultItem;
            default:
                throw new NotImplementedException("Item not edited");
        }
    }

    public async Task<List<Supplier>> GetAllSuppliers()
    {
        IQueryable<Supplier> query = _sharedContext.Suppliers;
        return await query.Take(4).ToListAsync();
    }
}

