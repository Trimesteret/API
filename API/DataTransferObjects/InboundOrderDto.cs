using API.Enums;
using API.Models.Orders;
using API.Models.Suppliers;

namespace API.DataTransferObjects;

public class InboundOrderDto
{
    public int? Id { get; set; }
    public double TotalPrice { get; set; }
    public DateTime? DeliveryDate { get; set; }
    public DateTime? OrderDate { get; set; }
    public List<OrderLineDto> OrderLines { get; set; }
    public InboundOrderState InboundOrderState { get; set; }
    public string? SupplierName { get; set; }
    public SupplierDto Supplier { get; set; }
}
