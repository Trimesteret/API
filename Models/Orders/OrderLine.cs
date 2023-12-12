using API.Models.Items;

namespace API.Models.Orders;

public class OrderLine
{
    public int Id { get; protected set; }
    public Item Item { get; protected set; }
    public double Price { get; protected set; }
    public int Quantity { get; protected set; }
    public PurchaseOrder PurchaseOrder { get; protected set; }

    /// <summary>
    /// Parameterless constructor for Entity Framework.
    /// </summary>
    public OrderLine()
    {

    }

    public OrderLine(Item item, int quantity, PurchaseOrder purchaseOrder)
    {
        this.Item = item;
        this.Quantity = quantity;
        this.Price = 0;
        this.PurchaseOrder = purchaseOrder;
    }
}
