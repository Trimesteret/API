using API.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace API.Models.Orders;

public abstract class Order
{
    public int Id { get; protected set; }
    public double TotalPrice { get; protected set; }
    public DateTime? DeliveryDate { get; protected set; }
    public DateTime? OrderDate { get; protected set; }

    public List<OrderLine> OrderLines { get; protected set; }


    /// <summary>
    /// Sets the order lines for the order
    /// </summary>
    /// <param name="context"></param>
    /// <param name="orderLines"></param>
    public void SetOrderLines(List<OrderLine> orderLines)
    {
        this.OrderLines = orderLines;
    }

    public void ClearOrderLines(SharedContext context)
    {
        this.OrderLines.Clear();
    }
}
