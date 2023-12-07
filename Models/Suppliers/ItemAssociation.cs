using API.DataTransferObjects;
using API.Models.Items;

namespace API.Models.Suppliers;

public class ItemAssociation
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int SupplierId { get; set; }
    
    public ItemAssociation()
    {
    }
    
    public ItemAssociation(Item item, int supplierId)
    {
        this.ItemId = item.Id;
        this.SupplierId = supplierId;
    }
}