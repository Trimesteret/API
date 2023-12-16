using API.DataTransferObjects;
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

    public InboundOrder(InboundOrderDto inboundOrderDto)
    {
        this.TotalPrice = inboundOrderDto.TotalPrice;
        this.DeliveryDate = inboundOrderDto.DeliveryDate;
        this.OrderDate = inboundOrderDto.OrderDate;
        this.OrderLines = inboundOrderDto.OrderLines.Select(orderLineDto => new OrderLine(orderLineDto)).ToList();
        this.InboundOrderState = inboundOrderDto.InboundOrderState;
    }

    public InboundOrder(DateTime? orderDate, DateTime? deliveryDate, InboundOrderState inboundOrderState)
    {
        this.OrderDate = orderDate;
        this.DeliveryDate = deliveryDate;
        this.TotalPrice = 0;
        this.InboundOrderState = inboundOrderState;
    }
}
