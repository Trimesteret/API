using API.DataTransferObjects;
using API.Models.Items;

namespace API.Models.Suppliers;

public class ItemRelation
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int SupplierId { get; set; }
    
    public ItemRelation()
    {
    }
    
    public ItemRelation(Item item, int supplierId)
    {
        this.ItemId = item.Id;
        this.SupplierId = supplierId;
    }
}