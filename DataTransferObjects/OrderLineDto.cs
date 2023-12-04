using API.Models.Items;
using API.Models.Orders;

namespace API.DataTransferObjects;

public class OrderLineDto
{
    public int? Id { get; set; }
    public Item Item { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
    public PurchaseOrder PurchaseOrder { get; set; } = null;
}
