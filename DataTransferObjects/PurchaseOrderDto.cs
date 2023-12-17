using API.Enums;
using API.Models.Authentication;
using API.Models.Orders;

namespace API.DataTransferObjects;

public class PurchaseOrderDto
{
    public int? Id { get; set; }
    public double TotalPrice { get; set; }

    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }

    public DateTime? DeliveryDate { get; set; }
    public DateTime? OrderDate { get; set; }

    public string AddressLine { get; set; }
    public string? Floor { get; set; }
    public string? Door { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public List<OrderLineDto> OrderLines { get; set; }
    public PurchaseOrderState PurchaseOrderState { get; set; }
}
