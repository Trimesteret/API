using API.DataTransferObjects;
using API.Models.Items;

namespace API.Models.Orders;

public class OrderLine
{
    public int Id { get; protected set; }
    public Item? Item { get; set; }
    public int ItemId { get; protected set; }
    public double LinePrice { get; protected set; } = 0;
    public double? ItemPrice { get; protected set; }
    public int Quantity { get; protected set; }

    /// <summary>
    /// Parameterless constructor for Entity Framework.
    /// </summary>
    public OrderLine()
    {
        this.Item = null!;
    }

    public OrderLine(Item item, int quantity)
    {
        this.Item = item;
        this.Quantity = quantity;
    }
}
