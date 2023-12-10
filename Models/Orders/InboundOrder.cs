using API.Models.Suppliers;

namespace API.Models.Orders;

public class InboundOrder : Order
{
    public Supplier? Supplier { get; protected set; }

    /**
     * Parameterless constructor for EF Core
     */
    public InboundOrder()
    {

    }
}
