using API.DataTransferObjects;
using API.Enums;
using API.Models.Authentication;

namespace API.Models.Orders;

public class PurchaseOrder: Order
{
    public PurchaseOrderState PurchaseOrderState { get; protected set; } = PurchaseOrderState.Open;
    public Customer Customer { get; protected set; }
    public Guest Guest { get; protected set; }

    public double TotalPrice { get; protected set; }

    /**
     * Parameterless constructor for EF Core
     */
    public PurchaseOrder()
    {
        this.Customer = null;
        this.Guest = null;
    }

    public PurchaseOrder(Customer customer)
    {
        this.Customer = customer;
        this.Guest = null;
    }

    public PurchaseOrder(Guest guest)
    {
        this.Guest = guest;
        this.Customer = null;
    }

    public void AddOrderLineToPurchaseOrder(OrderLineDto orderLineDto)
    {
        var orderLine = new OrderLine(orderLineDto.Item, orderLineDto.Quantity, this);
        this.OrderLines.Add(orderLine);
        this.TotalPrice += orderLine.Price;
    }
}
