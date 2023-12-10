namespace API.Models.Orders;

public abstract class Order
{
    public int Id { get; protected set; }
    public double TotalPrice { get; protected set; }
    public string? DeliveryDate { get; protected set; }
    public DateTime? OrderDate { get; protected set; }
    public List<OrderOrderLineRelation> OrderLinesRelations { get; protected set; }
}
