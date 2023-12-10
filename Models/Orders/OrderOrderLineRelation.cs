namespace API.Models.Orders;

public class OrderOrderLineRelation
{
    public int OrderOrderLineRelationId { get; protected set; }

    public int OrderId { get; protected set; }
    public Order Order { get; protected set; }

    public int OrderLineId { get; protected set; }
    public OrderLine? OrderLine { get; protected set; }

    /// <summary>
    /// Parameterless constructor for EF Core
    /// </summary>
    public OrderOrderLineRelation()
    {
        this.Order = null!;
    }

    public OrderOrderLineRelation(Order order, OrderLine orderLine)
    {
        this.Order = order;
        this.OrderId = order.Id;
        this.OrderLine = orderLine;
        this.OrderLineId = orderLine.Id;
    }
}
