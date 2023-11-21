using API.Models.Authentication;
using API.Models.Items;

namespace API.Models.Orders;

public class CustomerPurchaseOrder: Order
{
    public Customer Customer { get; protected set; }

    // Parameterless constructor for EF Core
    protected CustomerPurchaseOrder()
    {
    }


    public CustomerPurchaseOrder(Customer customer, List<Item> items, string? comment)
    {
        this.Customer = customer;
        this.Items.AddRange(items);
        this.Comment = comment;
    }
}
