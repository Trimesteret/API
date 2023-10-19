namespace API.Models;

public class InboundOrder: Order
{
    public int Id { get; set; }
    public Supplier Supplier { get; set; }
}
