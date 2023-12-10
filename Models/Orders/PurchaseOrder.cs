using API.DataTransferObjects;
using API.Enums;
using API.Models.Authentication;

namespace API.Models.Orders;

public class PurchaseOrder: Order
{
    public Customer? Customer { get; protected set; }
    public PurchaseOrderState PurchaseOrderState { get; protected set; } = PurchaseOrderState.Open;

    public double TotalPrice { get; protected set; }

    /// <summary>
    /// Parameterless constructor for Entity Framework.
    /// </summary>
    public PurchaseOrder()
    {
        this.OrderLinesRelations = new List<OrderOrderLineRelation>();
    }

    public PurchaseOrder(Customer customer)
    {
        this.Customer = customer;
        this.OrderLinesRelations = new List<OrderOrderLineRelation>();
    }

    public void AddOrderLineToPurchaseOrder(OrderLineDto orderLineDto)
    {
        var orderLine = new OrderLine(orderLineDto.Item, orderLineDto.Quantity);
        var orderOrderLineRelation = new OrderOrderLineRelation(this, orderLine);
        this.OrderLinesRelations.Add(orderOrderLineRelation);
        this.TotalPrice += orderLine.LinePrice;
    }
}
