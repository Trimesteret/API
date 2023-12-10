using API.Enums;
using API.Models.Orders;

namespace API.DataTransferObjects;

public class PurchaseOrderDto
{
    public int? Id { get; set; }
    public double TotalPrice { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public DateTime? OrderDate { get; set; }
    public Address DeliveryAddress { get; set; }
    public List<OrderLine> OrderLines { get; set; }
    public PurchaseOrderState PurchaseOrderState { get; set; }
}
