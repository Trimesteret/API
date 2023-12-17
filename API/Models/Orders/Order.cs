using Microsoft.EntityFrameworkCore;

namespace API.Models.Orders;

public abstract class Order
{
    public int Id { get; protected set; }
    public double TotalPrice { get; protected set; }
    public DateTime? DeliveryDate { get; protected set; }
    public DateTime? OrderDate { get; protected set; }

    public List<OrderLine> OrderLines { get; protected set; }

    public async Task<List<OrderLine>> GetOrderLines(SharedContext context)
    {
        return await context.OrderLines.Where(ol => ol.Id == this.Id).ToListAsync();
    }
}
