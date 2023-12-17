using API.Enums;

namespace API.Models.Authentication;

public class Employee : User
{
    /// <summary>
    /// Parameterless constructor for creating an an Admin
    /// </summary>
    public Employee()
    {

    }

    /// <summary>
    /// Constructor for creating an employee that is not signed up yet
    /// </summary>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <param name="phone"></param>
    /// <param name="email"></param>
    public Employee(string firstName, string lastName, string email, string phone)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Token = null;
        Role = Role.Employee;
        SignedUp = false;
    }

    /// <summary>
    /// Constructor for creating an employee from a user
    /// </summary>
    /// <param name="user"></param>
    public Employee(User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Phone = user.Phone;
        Email = user.Email;
        Password = user.Password;
        Token = user.Token;
        Salt = user.Salt;
        Role = Role.Employee;
        SignedUp = false;
    }
}
