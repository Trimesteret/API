using API.Models.Suppliers;

namespace API.Models.Orders;

public class InboundOrder: Order
{
    public int Id { get; set; }
    public Supplier Supplier { get; set; }
}
