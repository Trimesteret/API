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
        Role = Role.Customer;
    }

    public Customer(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Phone = user.Phone;
        Email = user.Email;
        Password = user.Password;
        Salt = user.Salt;
        Token = user.Token;
        CustomerPurchaseOrders = new List<CustomerPurchaseOrder>();
        Role = Role.Customer;
    }

    protected void ChangeCustomer(string firstName, string lastName, string phone, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Token = "";
        Role = Role.Customer;
    }

    public CustomerPurchaseOrder PlaceCustomerPurchaseOrder(List<Item> items, string comment)
    {
        var customerPurchaseOrder = new CustomerPurchaseOrder(this, items, comment);
        this.CustomerPurchaseOrders.Add(customerPurchaseOrder);
        return customerPurchaseOrder;
    }
}
