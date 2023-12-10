using API.Enums;
using API.Models.Suppliers;

namespace API.Models.Orders;

public class InboundOrder : Order
{
    public InboundOrderState InboundOrderState { get; set; }
    public Supplier? Supplier { get; protected set; }

    /// <summary>
    /// Parameterless constructor for EF Core
    /// </summary>
    public InboundOrder()
    {

    }

    public InboundOrder(DateTime? orderDate, DateTime? deliveryDate, InboundOrderState inboundOrderState)
    {
        this.OrderDate = orderDate;
        this.DeliveryDate = deliveryDate;
        this.OrderLinesRelations = new List<OrderOrderLineRelation>();
        this.TotalPrice = 0;
        this.InboundOrderState = inboundOrderState;
    }
}
