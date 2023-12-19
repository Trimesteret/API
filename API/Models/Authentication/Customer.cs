using API.DataTransferObjects;
using API.Enums;
using API.Models.Orders;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Models.Authentication;

public class Customer : User
{
    /// <summary>
    /// Empty constructor for entity framework core
    /// </summary>
    public Customer()
    {

    }

    /// <summary>
    /// Constructor for creating a customer that is signed up
    /// </summary>
    /// <param name="signupDto"></param>
    public Customer(SignupDto signupDto)
    {
        FirstName = signupDto.FirstName;
        LastName = signupDto.LastName;
        Phone = signupDto.Phone;
        Email = signupDto.Email.ToLower();
        Role = Role.Customer;
        Token = null;
        TokenExpiration = null;
        Salt = AuthenticationService.GenerateSalt();
        Password = AuthenticationService.HashPassword(signupDto.Password, Salt);
        SignedUp = true;
    }

    /// <summary>
    /// Used for creating a customer from a user standard dto
    /// </summary>
    /// <param name="userStandardDto"></param>
    public Customer(UserStandardDto userStandardDto)
    {
        FirstName = userStandardDto.FirstName;
        LastName = userStandardDto.LastName;
        Phone = userStandardDto.Phone;
        Email = userStandardDto.Email;
        Role = userStandardDto.Role;
        Token = null;
        TokenExpiration = null;
        Salt = null;
        Password = null;
        SignedUp = false;
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
        Token = user.Token;
        Role = Role.Customer;

        if (user.Password != null && user.Salt != null)
        {
            Salt = user.Salt;
            Password = user.Password;
            SignedUp = true;
        }
    }

    public async Task<List<PurchaseOrder>> GetPurchaseOrders(SharedContext context)
    {
        return await context.PurchaseOrders.Where(po => po.CustomerEmail == this.Email).ToListAsync();
    }
}
