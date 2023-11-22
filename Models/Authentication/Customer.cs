using API.Models.Items;
using API.Models.Orders;

namespace API.Models.Authentication;

public class Customer : User
{
    public int? PhoneNumber { get; protected set; }

    public List<CustomerPurchaseOrder> CustomerPurchaseOrders { get; protected set; }

    public Customer(string firstName, string lastName, int phone, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Token = "";
        CustomerPurchaseOrders = new List<CustomerPurchaseOrder>();
    }

    override
    public string GetClassName()
    {
        return "Customer";
    }

    protected void ChangeCustomer(string firstName, string lastName, int phone, string email, string password, int? phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Token = "";
        PhoneNumber = phoneNumber;
    }

    public CustomerPurchaseOrder PlaceCustomerPurchaseOrder(List<Item> items, string comment)
    {
        var customerPurchaseOrder = new CustomerPurchaseOrder(this, items, comment);
        this.CustomerPurchaseOrders.Add(customerPurchaseOrder);
        return customerPurchaseOrder;
    }
}
