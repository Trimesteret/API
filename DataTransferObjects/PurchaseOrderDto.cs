using API.Enums;
using API.Models.Authentication;
using API.Models.Orders;

namespace API.DataTransferObjects;

public class PurchaseOrderDto
{
    public int? Id { get; set; }
    public double TotalPrice { get; set; }
    public User user;
    public DateTime? DeliveryDate { get; set; }
    public DateTime? OrderDate { get; set; }
    public Address Address { get; set; }
    public List<OrderLineDto> OrderLines { get; set; }
    public PurchaseOrderState PurchaseOrderState { get; set; }
}
