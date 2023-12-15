using API.Enums;
using API.Models.Orders;

namespace API.Models.Authentication;

public class Customer : User
{
    public new string? Password { get; protected set; }
    public new Byte[]? Salt { get; protected set; }
    public new string? Token { get; protected set; }
    public List<PurchaseOrder> PurchaseOrders { get; protected set; }

    public Customer(string firstName, string lastName, string phone, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        PurchaseOrders = new List<PurchaseOrder>();
        Role = Role.Customer;
    }

    public Customer(string firstName, string lastName, string phone, string email, string password, Byte[] salt)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Salt = salt;
        PurchaseOrders = new List<PurchaseOrder>();
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
        PurchaseOrders = new List<PurchaseOrder>();
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
}
