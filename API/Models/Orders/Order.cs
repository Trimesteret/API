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
    /// Gets order lines associated with this order
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<List<OrderLine>> GetOrderLines(SharedContext context)
    {
        var orderLines = await context.InboundOrders.Where(order => order.Id == this.Id).Include(order => order.OrderLines).
            Select(order => order.OrderLines).FirstOrDefaultAsync();

        return orderLines ?? new List<OrderLine>();
    }

    /// <summary>
    /// Clears order lines associated with this order
    /// </summary>
    /// <param name="context"></param>
    public async Task ClearOrderLines(SharedContext context)
    {
        var relatedOrderLines = await this.GetOrderLines(context);
        context.OrderLines.RemoveRange(relatedOrderLines);
        await context.SaveChangesAsync();
    }
}
