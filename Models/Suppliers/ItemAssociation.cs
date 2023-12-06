using API.DataTransferObjects;
using API.Models.Items;

namespace API.Models.Suppliers;

public class ItemAssociation
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public Item ItemRef { get; set; }
    public int SupplierId { get; set; }
    public Supplier SupplierRef { get; set; }
    
    public ItemAssociation()
    {
    }
    
    public ItemAssociation(Item item, Supplier supplier)
    {
        this.ItemId = item.Id;
        this.ItemRef = item;
        this.SupplierId = supplier.Id;
        this.SupplierRef = supplier;
    }
}