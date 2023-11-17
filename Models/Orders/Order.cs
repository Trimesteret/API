using API.Models.Items;

namespace API.Models.Orders;

public class Order
{
    public int Id { get; protected set; }
    public Item[]? Items { get; protected set; }
    public string? Comment { get; protected set; }
}
