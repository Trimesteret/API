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

    /// <summary>
    /// Method for changing the properties of a supplier
    /// </summary>
    /// <param name="name"></param>
    public void ChangeSupplierProperties(string name)
    {
        this.Name = name;
    }

    /// <summary>
    /// Sets items associated with this supplier
    /// </summary>
    /// <param name="context">The context</param>
    /// <param name="associatedItems"></param>
    public async Task SetAssociatedItems(SharedContext context, List<ItemDto>? associatedItems)
    {
        await this.ClearAssociatedItems(context);

        this.SupplierItemRelations = associatedItems?.Select(item => new SupplierItemRelation()
        {
            ItemId = item.Id,
            SupplierId = this.Id,
        }).ToList();
    }

    /// <summary>
    /// Gets items associated with this supplier
    /// </summary>
    /// <param name="context">The context</param>
    /// <returns></returns>
    public async Task<List<Item>> GetAssociatedItems(SharedContext context)
    {
        return await context.Items
            .Where(item => context.SupplierItemRelations.Any(sir => sir.SupplierId == this.Id && sir.ItemId == item.Id))
            .ToListAsync();
    }

    /// <summary>
    /// Gets items associated with this supplier as a list of ints
    /// </summary>
    /// <param name="context">The context</param>
    /// <returns></returns>
    public async Task<List<int>> GetAssociatedItemsAsIntList(SharedContext context)
    {
        return await context.Items
            .Where(item => context.SupplierItemRelations.Any(sir => sir.SupplierId == this.Id && sir.ItemId == item.Id))
            .Select(item => item.Id)
            .ToListAsync();
    }

    /// <summary>
    /// Clears the list of associated items and removes them from the database
    /// </summary>
    /// <param name="context">The context</param>
    public async Task ClearAssociatedItems(SharedContext context)
    {
        var supplierItemRelations = await context.SupplierItemRelations.Where(sir => sir.SupplierId == this.Id).ToListAsync();
        context.SupplierItemRelations.RemoveRange(supplierItemRelations);
        await context.SaveChangesAsync();
    }

}
