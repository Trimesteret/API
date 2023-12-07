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

    public SupplierService(SharedContext dbSharedContext)
    {
        _sharedContext = dbSharedContext;

    }
    
    public async Task<Supplier> GetSupplierById(int id)
    {
        Supplier? supplier = await _sharedContext.Suppliers.FirstOrDefaultAsync(i => i.Id == id);
        if (supplier != null)
        {
            
            supplier.Items = await GetAssociated(supplier.Id);
            return supplier;
        }
        return null;
    }

    public async Task<List<ItemAssociation>> GetAssociated(int id)
    {
        var itemAssociations = await _sharedContext.ItemAssociations
            .Where(i => i.SupplierId == id)
            .ToListAsync();
        Console.WriteLine(itemAssociations);
        List<ItemAssociation> itemList = new List<ItemAssociation>();
        foreach (var association in itemAssociations)
        {
            var item = await _sharedContext.Items
                .FirstOrDefaultAsync(i => i.Id == association.ItemId);
            if (item != null)
            {
                ItemAssociation add = new ItemAssociation();
                add.ItemId = item.Id;
                itemList.Add(add);
            }
        }
        return itemList;
    }
    
    public async Task<Supplier> CreateSupplier(SupplierDto supplierDto)
    {
        Supplier newSupplier = new Supplier(supplierDto.Name);
        
        _sharedContext.Suppliers.Add(newSupplier);
        await _sharedContext.SaveChangesAsync();

        if (supplierDto.Items != null && supplierDto.Items.Any())
        {
            foreach (ItemAssociation item in supplierDto.Items)
            {
                await AssociateItems(newSupplier, item);
            }
        }
        
        return newSupplier;
    }
    public async Task<ItemAssociation> AssociateItems(Supplier supplier, ItemAssociation itemAssociation)
    {
        if (supplier.Id == 0)
        {
            _sharedContext.Suppliers.Add(supplier);  
        }
        else
        {
            _sharedContext.Entry(supplier).State = EntityState.Unchanged;
        }
        
        if (itemAssociation.Id == 0)
        {
            _sharedContext.ItemAssociations.Add(itemAssociation); 
        }
        else
        {
            _sharedContext.Entry(itemAssociation).State = EntityState.Unchanged;
        }
        
        itemAssociation.SupplierId = supplier.Id;
        
        await _sharedContext.SaveChangesAsync();

        return itemAssociation;
    }

    public async Task<List<Supplier>> GetAllSuppliers()
    {
        return await _sharedContext.Suppliers.ToListAsync();
    }
}
