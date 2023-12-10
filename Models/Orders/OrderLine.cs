using API.Models.Items;

namespace API.Models.Orders;

public class OrderLine
{
    public int Id { get; protected set; }
    public Item Item { get; protected set; }
    public int ItemId { get; protected set; }
    public double LinePrice { get; protected set; } = 0;
    public double? ItemPrice { get; protected set; }
    public int Quantity { get; protected set; }

    /**
     * Parameterless constructor for EF Core
     */
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
