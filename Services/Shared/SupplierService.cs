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
            
            supplier.Items = await GetRelations(supplier.Id);
            return supplier;
        }
        return null;
    }

    public async Task<List<ItemRelation>> GetRelations(int id)
    {
        var itemRelations = await _sharedContext.ItemRelations
            .Where(i => i.SupplierId == id)
            .ToListAsync();
        Console.WriteLine(itemRelations);
        List<ItemRelation> itemList = new List<ItemRelation>();
        foreach (var relation in itemRelations)
        {
            var item = await _sharedContext.Items
                .FirstOrDefaultAsync(i => i.Id == relation.ItemId);
            if (item != null)
            {
                ItemRelation add = new ItemRelation();
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
            foreach (ItemRelation item in supplierDto.Items)
            {
                await RelateItems(newSupplier, item);
            }
        }
        
        return newSupplier;
    }
    public async Task<ItemRelation> RelateItems(Supplier supplier, ItemRelation itemRelation)
    {
        if (supplier.Id == 0)
        {
            _sharedContext.Suppliers.Add(supplier);  
        }
        else
        {
            _sharedContext.Entry(supplier).State = EntityState.Unchanged;
        }
        
        if (itemRelation.Id == 0)
        {
            _sharedContext.ItemRelations.Add(itemRelation); 
        }
        else
        {
            _sharedContext.Entry(itemRelation).State = EntityState.Unchanged;
        }
        
        itemRelation.SupplierId = supplier.Id;
        
        await _sharedContext.SaveChangesAsync();

        return itemRelation;
    }

    public async Task<List<Supplier>> GetAllSuppliers()
    {
        return await _sharedContext.Suppliers.ToListAsync();
    }
}
