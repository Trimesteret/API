namespace API.Models.Orders;

public class OrderOrderLineRelation
{
    public int OrderOrderLineRelationId { get; set; }

    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int OrderLineId { get; set; }
    public OrderLine? OrderLine { get; set; }

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
