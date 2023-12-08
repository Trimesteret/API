using API.DataTransferObjects;
using API.Models.Items;
using Microsoft.EntityFrameworkCore;

namespace API.Models.Suppliers;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<SupplierItemRelation>? SupplierItemRelations { get; set; }

    public Supplier(string name)
    {
        this.Name = name;
    }

    public void ChangeSupplierProperties(string name)
    {
        this.Name = name;
    }

    public async Task SetAssociatedItems(SharedContext context, List<ItemDto>? associatedItems)
    {
        await this.ClearAssociatedItems(context);

        this.SupplierItemRelations = associatedItems?.Select(item => new SupplierItemRelation()
        {
            ItemId = item.Id,
            SupplierId = this.Id,
        }).ToList();
    }

    public async Task<List<Item>> GetAssociatedItems(SharedContext context)
    {
        return await context.Items
            .Where(item => context.SupplierItemRelations.Any(sir => sir.SupplierId == this.Id && sir.ItemId == item.Id))
            .ToListAsync();
    }

    public async Task<List<int>> GetAssociatedItemsAsIntList(SharedContext context)
    {
        return await context.Items
            .Where(item => context.SupplierItemRelations.Any(sir => sir.SupplierId == this.Id && sir.ItemId == item.Id))
            .Select(item => item.Id)
            .ToListAsync();
    }

    public async Task ClearAssociatedItems(SharedContext context)
    {
        var supplierItemRelations = await context.SupplierItemRelations.Where(sir => sir.SupplierId == this.Id).ToListAsync();
        context.SupplierItemRelations.RemoveRange(supplierItemRelations);
    }

}
