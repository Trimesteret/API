using API.Enums;
using API.Models.Items;
using API.Models.Orders;

namespace API.Models.Authentication;

public class Customer : User
{
    public List<CustomerPurchaseOrder> CustomerPurchaseOrders { get; protected set; }

    public Customer(string firstName, string lastName, string phone, string email, string password, Byte[] salt)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Salt = salt;
        Token = "";
        CustomerPurchaseOrders = new List<CustomerPurchaseOrder>();
    }

    override
    public Roles GetClassRoleEnum()
    {
        return Roles.Customer;
    }

    protected void ChangeCustomer(string firstName, string lastName, string phone, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Token = "";
    }

    public CustomerPurchaseOrder PlaceCustomerPurchaseOrder(List<Item> items, string comment)
    {
        var customerPurchaseOrder = new CustomerPurchaseOrder(this, items, comment);
        this.CustomerPurchaseOrders.Add(customerPurchaseOrder);
        return customerPurchaseOrder;
    }
}
