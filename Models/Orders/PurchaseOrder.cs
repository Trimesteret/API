using API.DataTransferObjects;
using API.Enums;
using API.Models.Authentication;

namespace API.Models.Orders;

public class PurchaseOrder: Order
{
    public Address? DeliveryAddress { get; protected set; }
    public Customer? Customer { get; protected set; }
    public PurchaseOrderState PurchaseOrderState { get; protected set; }

    public double TotalPrice { get; protected set; }

    /// <summary>
    /// Parameterless constructor for Entity Framework.
    /// </summary>
    public PurchaseOrder()
    {
        this.OrderLinesRelations = new List<OrderOrderLineRelation>();
    }

    public PurchaseOrder(DateTime? orderDate, DateTime? deliveryDate, Address? deliveryAddress, PurchaseOrderState purchaseOrderState)
    {
        this.OrderDate = orderDate;
        this.DeliveryDate = deliveryDate;
        this.DeliveryAddress = deliveryAddress;
        this.OrderLinesRelations = new List<OrderOrderLineRelation>();
        this.TotalPrice = 0;
        this.PurchaseOrderState = purchaseOrderState;
    }

    public void SetCustomer(Customer customer)
    {
        this.Customer = customer;
    }

    public void AddOrderLineToPurchaseOrder(OrderLineDto orderLineDto)
    {
        var orderLine = new OrderLine(orderLineDto.Item, orderLineDto.Quantity);
        var orderOrderLineRelation = new OrderOrderLineRelation(this, orderLine);
        this.OrderLinesRelations.Add(orderOrderLineRelation);
        this.TotalPrice += orderLine.LinePrice;
    }
}
