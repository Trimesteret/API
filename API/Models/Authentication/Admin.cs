using API.DataTransferObjects;
using API.Enums;
using API.Models.Items;

namespace API.Models.Authentication;

public class Admin : Employee
{
    /// <summary>
    /// Empty constructor for entity framework core
    /// </summary>
    public Admin()
    {

    }

    /// <summary>
    /// Constructor for creating a new admin that is not signed up yet
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="phone"></param>
    /// <param name="email"></param>
    public Admin(string firstName, string lastName, string email, string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Role = Role.Admin;
        SignedUp = false;
        Token = null;
        TokenExpiration = null;
        SignedUp = false;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="user"></param>
    public Admin(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Phone = user.Phone;
        Email = user.Email;
        Password = user.Password;
        Token = user.Token;
        Salt = user.Salt;
        Role = Role.Admin;
        SignedUp = false;
    }
}
