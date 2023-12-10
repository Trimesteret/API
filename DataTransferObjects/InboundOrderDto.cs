using API.Enums;
using API.Models.Orders;

namespace API.DataTransferObjects;

public class InboundOrderDto
{
    public int? Id { get; set; }
    public double TotalPrice { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public DateTime? OrderDate { get; set; }
    public List<OrderLine> OrderLines { get; set; }
    public InboundOrderState InboundOrderState { get; set; }
}
