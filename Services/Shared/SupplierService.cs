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
        Supplier newSupplier = new Supplier(supplierDto.Name);
        await _sharedContext.Suppliers.AddAsync(newSupplier);
        await _sharedContext.SaveChangesAsync();
        
        if(supplierDto.Items != null)
        {
            Supplier supplier = await _sharedContext.Suppliers.FirstOrDefaultAsync(supplier => supplier.Name == supplierDto.Name);
            foreach (ItemDto item in supplierDto.Items)
            {
                await AssociateItems(supplier, item);
            }
        }
        return newSupplier;
    }

    public async Task<ItemAssociation> AssociateItems(Supplier supplier, ItemDto itemDto)
    {
        Item item = ItemConstructor(itemDto);
        var itemAssociation = new ItemAssociation(item, supplier);
        await _sharedContext.ItemAssociations.AddAsync(itemAssociation);
        await _sharedContext.SaveChangesAsync();
        return itemAssociation;
        
    }

    public Item ItemConstructor(ItemDto itemDto)
    {
        switch (itemDto.ItemType)
        {
            case ItemType.Wine:
                Wine wine = new Wine(itemDto.ItemName, itemDto.Ean, itemDto.ItemQuantity, itemDto.Price,
                    itemDto.ItemDescription, itemDto.ItemType, itemDto.WineType, itemDto.Year, itemDto.Volume,
                    itemDto.AlcoholPercentage, itemDto.Country, itemDto.Region, itemDto.GrapeSort, itemDto.Winery,
                    itemDto.TastingNotes, itemDto.SuitableFor);
                return wine;
            case ItemType.Liquor:
                Liquor liquor = new Liquor(itemDto.ItemName, itemDto.Ean, itemDto.ItemQuantity, itemDto.Price,
                    itemDto.ItemDescription, itemDto.ItemType);
                return liquor;
            case ItemType.DefaultItem:
                DefaultItem defaultItem = new DefaultItem(itemDto.ItemName, itemDto.Ean, itemDto.ItemQuantity,
                    itemDto.Price, itemDto.ItemDescription, itemDto.ItemType);
                return defaultItem;
            default:
                throw new NotImplementedException("Item not created");
        }
    }

    // public async Task<Item> EditItemList(ItemDto itemDto, int supplierId)
    // {
    //     var itemToEdit = await _sharedContext.Items.FirstOrDefaultAsync(itemToEdit => itemToEdit.Id == itemDto.Id);
    //     itemDto.SupplierId = supplierId;
    //     switch (itemDto.ItemType)
    //     {
    //
    //         case ItemType.Wine:
    //             Wine wine = (Wine)itemToEdit;
    //             wine.ChangeWineProperties(itemDto.ItemName, itemDto.SupplierId, itemDto.Ean, itemDto.ItemQuantity, itemDto.Price,
    //                 itemDto.ItemDescription, itemDto.ItemType, itemDto.WineType, itemDto.Year, itemDto.Volume,
    //                 itemDto.AlcoholPercentage, itemDto.Country, itemDto.Region, itemDto.GrapeSort, itemDto.Winery,
    //                 itemDto.TastingNotes, itemDto.SuitableFor);
    //             await _sharedContext.SaveChangesAsync();
    //             return wine;
    //         case ItemType.Liquor:
    //             Liquor liquor = (Liquor)itemToEdit;
    //             liquor.ChangeLiquorProperties(itemDto.ItemName, itemDto.SupplierId, itemDto.Ean, itemDto.ItemQuantity, itemDto.Price,
    //                 itemDto.ItemDescription, itemDto.ItemType);
    //             await _sharedContext.SaveChangesAsync();
    //             return liquor;
    //         case ItemType.DefaultItem:
    //             DefaultItem defaultItem = (DefaultItem)itemToEdit;
    //             defaultItem.ChangeDefaultItemProperties(itemDto.ItemName, itemDto.SupplierId, itemDto.Ean, itemDto.ItemQuantity,
    //                 itemDto.Price, itemDto.ItemDescription, itemDto.ItemType);
    //             await _sharedContext.SaveChangesAsync();
    //             return defaultItem;
    //         default:
    //             throw new NotImplementedException("Item not edited");
    //     }
    // }

    public async Task<List<Supplier>> GetAllSuppliers()
    {
        IQueryable<Supplier> query = _sharedContext.Suppliers;
        return await query.Take(4).ToListAsync();
    }
}

