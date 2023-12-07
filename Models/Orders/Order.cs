using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using API.Enums;
using API.Models.Items;

namespace API.Models.Orders;

public abstract class Order
{
    public int Id { get; protected set; }
    public string Comment { get; protected set; }
    public string Discriminator { get; protected set; }
    public PurchaseOrderState PurchaseOrderState { get; protected set; } = PurchaseOrderState.Open;
    public double TotalPrice { get; protected set; }
    public string? DeliveryDate { get; protected set; }
    public DateTime? OrderDate { get; protected set; }
    public string Supplier { get; protected set; }
    public List<OrderLine> OrderLines { get; protected set; }
}
