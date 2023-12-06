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
        Supplier supplier = await _sharedContext.Suppliers.FirstOrDefaultAsync(i => i.Id == id);
        return supplier;
    }

    public async Task<List<Item>> GetAssociated(int id)
    {
        var itemAssociations = await _sharedContext.ItemAssociations
            .Where(i => i.SupplierId == id)
            .ToListAsync();
        Console.WriteLine(itemAssociations);
        List<Item> itemList = new List<Item>();
        foreach (var association in itemAssociations)
        {
            var item = await _sharedContext.Items
                .FirstOrDefaultAsync(i => i.Id == association.ItemId);
            if (item != null)
            {
                itemList.Add(item);
            }
        }
        return itemList;
    }


    public async Task<Supplier> CreateSupplier(SupplierDto supplierDto)
    {
        Supplier newSupplier = new Supplier(supplierDto.Name);
        await _sharedContext.Suppliers.AddAsync(newSupplier);
        await _sharedContext.SaveChangesAsync();

        if (supplierDto.Items != null)
        {
            Supplier supplier =
                await _sharedContext.Suppliers.FirstOrDefaultAsync(supplier => supplier.Name == supplierDto.Name);
            Console.WriteLine(supplier.Id);
            foreach (ItemDto item in supplierDto.Items)
            {
                await AssociateItems(supplier, item);
            }
        }

        return newSupplier;
    }

    public async Task<ItemAssociation> AssociateItems(Supplier supplier, ItemDto itemDto)
    {
        // Item item = await _sharedContext.Items.FirstOrDefaultAsync(item => item == item);
        var itemToEdit = await _sharedContext.Items.FirstOrDefaultAsync(item => itemDto.Id == item.Id);
        var itemAssociation = new ItemAssociation(itemToEdit, supplier);
        await _sharedContext.ItemAssociations.AddAsync(itemAssociation);
        await _sharedContext.SaveChangesAsync();
        return itemAssociation;
    }

    public async Task<List<Supplier>> GetAllSuppliers()
    {
        // IQueryable<Supplier> query = _sharedContext.Suppliers;
        // return await query.Take().ToListAsync();    
        return await _sharedContext.Suppliers.ToListAsync();
    }
}
