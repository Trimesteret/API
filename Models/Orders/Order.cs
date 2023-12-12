using API.Models.Items;

namespace API.Models.Orders;

public abstract class Order
{
    public int Id { get; protected set; }
    public string Comment { get; protected set; }
    public List<OrderLine> OrderLines { get; protected set; }
}
