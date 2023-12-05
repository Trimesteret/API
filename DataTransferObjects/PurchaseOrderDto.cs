using API.Enums;

namespace API.DataTransferObjects;

public class PurchaseOrderDto
{
    public PurchaseOrderState PurchaseOrderState { get; set; }
    public int Id { get; set; }
    public int OrderDate { get; set; }
    public int DeliveryDate { get; set; }
    public string DeliveryAddress { get; set; }
    public int TotalPrice { get; set; }
    public string Status { get; set; }
    public OrderLineDto OrderLine { get; set; }
    public string Supplier { get; set; }
}