using Microsoft.EntityFrameworkCore;

namespace API.Models.Orders;

public abstract class Order
{
    public int Id { get; protected set; }
    public double TotalPrice { get; protected set; }
    public DateTime? DeliveryDate { get; protected set; }
    public DateTime? OrderDate { get; protected set; }
    public List<OrderOrderLineRelation> OrderLinesRelations { get; protected set; }

    /// <summary>
    /// Gets the list of suitable for enums
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<List<OrderLine>> GetOrderLines(SharedContext context)
    {
        var orderLines = await context.OrderOrderLineRelations.Where(olr => olr.OrderId == this.Id)
            .Include(olr => olr.OrderLine)
            .ThenInclude(ol => ol.Item).Select(ol => ol.OrderLine).ToListAsync();

        return orderLines;
    }


    /// <summary>
    /// Sets the order lines for the order
    /// </summary>
    /// <param name="context"></param>
    /// <param name="orderLines"></param>
    public async Task SetOrderLines(SharedContext context, List<OrderLine> orderLines)
    {
        await this.ClearOrderLines(context);

        this.OrderLinesRelations = orderLines.Select(ol => new OrderOrderLineRelation()
        {
            OrderId = this.Id,
            Order = this,
            OrderLineId = ol.Id,
            OrderLine = ol
        }).ToList();

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Clears the list of order lines and removes them from the database
    /// </summary>
    /// <param name="context"></param>
    public async Task ClearOrderLines(SharedContext context)
    {
        var orderOrderLineRelations = await context.OrderOrderLineRelations.Where(oolr => oolr.OrderId == this.Id).ToListAsync();
        context.OrderOrderLineRelations.RemoveRange(orderOrderLineRelations);
        await context.SaveChangesAsync();
    }
}
