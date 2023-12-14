using API.Models.Items;
using API.Models.Orders;

namespace API.DataTransferObjects;

public class OrderLineDto
{
    public int? Id { get; set; }
    public ItemDto? ItemDto { get; set; }
    public int ItemId { get; set; }
    public double LinePrice { get; set; }
    public double ItemPrice { get; set; }
    public int Quantity { get; set; }
}
