using API.Models.Items;

namespace API.Models.Orders;

public abstract class Order
{
    public int Id { get; protected set; }
    public List<Item> Items { get; protected set; } = new List<Item>();
    public string? Comment { get; protected set; }
}
