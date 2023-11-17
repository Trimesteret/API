using API.Models.Authentication;
using API.Models.Items;

namespace API.Models.Orders;

public class CustomerPurchaseOrder: Order
{
    public Customer Customer { get; set; }

    public CustomerPurchaseOrder(Customer customer, Item[] items, string? comment)
    {
        this.Customer = customer;
        this.Items = items;
        this.Comment = comment;
    }
}
