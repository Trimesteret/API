using API.DataTransferObjects;
using API.Enums;
using API.Models.Authentication;

namespace API.Models.Orders;

public class PurchaseOrder: Order
{
    public string CustomerFirstName { get; set; }
    public string CustomerLastName { get; set; }
    public string CustomerPhone { get; set; }
    public string CustomerEmail { get; set; }

    public string AddressLine { get; set; }
    public string? Floor { get; set; }
    public string? Door { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public PurchaseOrderState PurchaseOrderState { get; protected set; }

    public double TotalPrice { get; protected set; }

    /// <summary>
    /// Parameterless constructor for Entity Framework.
    /// </summary>
    public PurchaseOrder()
    {

    }

    public PurchaseOrder(PurchaseOrderDto purchaseOrderDto)
    {
        this.CustomerFirstName = purchaseOrderDto.CustomerFirstName;
        this.CustomerLastName = purchaseOrderDto.CustomerLastName;
        this.CustomerPhone = purchaseOrderDto.CustomerPhone;
        this.CustomerEmail = purchaseOrderDto.CustomerEmail;
        this.AddressLine = purchaseOrderDto.AddressLine;
        this.Floor = purchaseOrderDto.Floor;
        this.Door = purchaseOrderDto.Door;
        this.PostalCode = purchaseOrderDto.PostalCode;
        this.City = purchaseOrderDto.City;
        this.Country = purchaseOrderDto.Country;
        this.OrderLines = purchaseOrderDto.OrderLines.Select(orderLineDto => new OrderLine(orderLineDto)).ToList();
        this.PurchaseOrderState = purchaseOrderDto.PurchaseOrderState;
        this.TotalPrice = purchaseOrderDto.TotalPrice;
        this.OrderDate = purchaseOrderDto.OrderDate;
    }

    public void ChangeOrderProperties(PurchaseOrderDto purchaseOrderDto)
    {
        this.CustomerFirstName = purchaseOrderDto.CustomerFirstName;
        this.CustomerLastName = purchaseOrderDto.CustomerLastName;
        this.CustomerPhone = purchaseOrderDto.CustomerPhone;
        this.CustomerEmail = purchaseOrderDto.CustomerEmail;
        this.AddressLine = purchaseOrderDto.AddressLine;
        this.Floor = purchaseOrderDto.Floor;
        this.Door = purchaseOrderDto.Door;
        this.PostalCode = purchaseOrderDto.PostalCode;
        this.City = purchaseOrderDto.City;
        this.Country = purchaseOrderDto.Country;
        this.OrderLines = purchaseOrderDto.OrderLines.Select(orderLineDto => new OrderLine(orderLineDto)).ToList();
        this.PurchaseOrderState = purchaseOrderDto.PurchaseOrderState;
        this.TotalPrice = purchaseOrderDto.TotalPrice;
        this.OrderDate = purchaseOrderDto.OrderDate;
    }
}
