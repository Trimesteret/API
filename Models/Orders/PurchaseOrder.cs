using API.DataTransferObjects;
using API.Enums;
using API.Models.Authentication;

namespace API.Models.Orders;

public class PurchaseOrder: Order
{
    public PurchaseOrderState PurchaseOrderState { get; protected set; } = PurchaseOrderState.Open;
    public Customer? Customer { get; protected set; }

    public double TotalPrice { get; protected set; }

    /// <summary>
    /// Parameterless constructor for Entity Framework.
    /// </summary>
    public PurchaseOrder()
    {

    }

    public PurchaseOrder(Customer customer)
    {
        this.Customer = customer;
    }

    public void AddOrderLineToPurchaseOrder(OrderLineDto orderLineDto)
    {
        var orderLine = new OrderLine(orderLineDto.Item, orderLineDto.Quantity, this);
        this.OrderLines.Add(orderLine);
        this.TotalPrice += orderLine.Price;
    }
}
