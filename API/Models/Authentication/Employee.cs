using API.DataTransferObjects;
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
    /// <param name="userStandardDto"></param>
    public Employee(UserStandardDto userStandardDto)
    {
        FirstName = userStandardDto.FirstName;
        LastName = userStandardDto.LastName;
        Phone = userStandardDto.Phone;
        Email = userStandardDto.Email;
        Token = null;
        Role = Role.Employee;
        TokenExpiration = null;
        Salt = null;
        Password = null;
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
