using API.Enums;
using API.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace API.Models.Authentication;

public class Customer : User
{
    /// <summary>
    /// Constructor for creating a customer that is signed up
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="email"></param>
    /// <param name="phone"></param>
    /// <param name="password"></param>
    /// <param name="salt"></param>
    public Customer(string firstName, string lastName, string phone, string email, string? password, Byte[]? salt)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Role = Role.Customer;
        Token = null;
        TokenExpiration = null;
        if (password != null)
        {
            Password = password;
        }

        if (salt != null)
        {
            Salt = salt;
        }

        SignedUp = password != null || salt != null;
    }

    /// <summary>
    /// A constructor for creating a customer from a user
    /// </summary>
    /// <param name="user"></param>
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
        Role = Role.Customer;
        SignedUp = false;
    }

    public async Task<List<PurchaseOrder>> GetPurchaseOrders(SharedContext context)
    {
        return await context.PurchaseOrders.Where(po => po.CustomerEmail == this.Email).ToListAsync();
    }
}
