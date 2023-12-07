using API.Models.Suppliers;
using API.DataTransferObjects;
using API.Models;
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
            supplier.Items = await GetRelationsList(supplier.Id);
            return supplier;
        }
        return null;
    }

    public async Task<Supplier> EditSupplier(SupplierDto supplierDto)
    {
        var existingSupplier =
            await _sharedContext.Suppliers.FirstOrDefaultAsync(supplier => supplier.Id == supplierDto.Id);
        await CheckRelations(existingSupplier, supplierDto);
        existingSupplier.ChangeSupplierProperties(supplierDto.Name, supplierDto?.Items);
        await _sharedContext.SaveChangesAsync();
        return existingSupplier;
    }

    public async Task CheckRelations(Supplier supplier, SupplierDto supplierDto)
    {
        List<ItemRelation> relationsList = new List<ItemRelation>();
        if (supplierDto.Items != null)
        {
            foreach (ItemRelation relation in supplierDto.Items)
            {
                await RelateItems(supplier, relation);
            }
        }
    }

    public async Task<List<ItemRelation>> GetRelationsList(int id)
    {
        var itemRelations = await _sharedContext.ItemRelations
            .Where(i => i.SupplierId == id)
            .ToListAsync();
        return itemRelations;
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
