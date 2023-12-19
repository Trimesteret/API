using API.DataTransferObjects;
using API.Enums;
using API.Models.Suppliers;

namespace API.Models.Orders;

public class InboundOrder : Order
{
    public InboundOrderState InboundOrderState { get; protected set; }
    public Supplier Supplier { get; protected set; }

    /// <summary>
    /// Parameterless constructor for EF Core
    /// </summary>
    public InboundOrder()
    {

    }

    public InboundOrder(InboundOrderDto inboundOrderDto, Supplier supplier)
    {
        this.TotalPrice = inboundOrderDto.TotalPrice;
        this.DeliveryDate = inboundOrderDto.DeliveryDate;
        this.OrderDate = inboundOrderDto.OrderDate;
        this.OrderLines = inboundOrderDto.OrderLines.Select(orderLineDto => new OrderLine(orderLineDto)).ToList();
        this.InboundOrderState = inboundOrderDto.InboundOrderState;
        this.Supplier = supplier;
    }

    /// <summary>
    /// Changes the properties of an inbound order
    /// </summary>
    /// <param name="inboundOrderDto"></param>
    public void ChangeInboundOrderProperties(InboundOrderDto inboundOrderDto)
    {
        this.TotalPrice = inboundOrderDto.TotalPrice;
        this.DeliveryDate = inboundOrderDto.DeliveryDate;
        this.OrderDate = inboundOrderDto.OrderDate;
        this.OrderLines = inboundOrderDto.OrderLines.Select(orderLineDto => new OrderLine(orderLineDto)).ToList();
        this.InboundOrderState = inboundOrderDto.InboundOrderState;
    }
}
