using API.DataTransferObjects;
using API.Enums;
using API.Models.Authentication;

namespace API.Models.Orders;

public class PurchaseOrder: Order
{
    public Address? Address { get; protected set; }
    public Customer? Customer { get; protected set; }
    public PurchaseOrderState PurchaseOrderState { get; protected set; }

    public double TotalPrice { get; protected set; }

    /// <summary>
    /// Parameterless constructor for Entity Framework.
    /// </summary>
    public PurchaseOrder()
    {

    }

    public PurchaseOrder(DateTime? orderDate, DateTime? deliveryDate, Address? deliveryAddress, PurchaseOrderState purchaseOrderState)
    {
        this.OrderDate = orderDate;
        this.DeliveryDate = deliveryDate;
        this.Address = deliveryAddress;
        this.TotalPrice = 0;
        this.PurchaseOrderState = purchaseOrderState;
    }

    public void SetCustomer(Customer customer)
    {
        this.Customer = customer;
    }
}
