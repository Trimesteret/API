using API.DataTransferObjects;
using API.Enums;

namespace API.Models.Authentication;

public abstract class User
{
    public int Id { get; protected set; }
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public string Phone { get; protected set; }
    public string Email { get; protected set; }
    public string Password { get; protected set; }
    public string Token { get; protected set; }
    public DateTime? TokenExpiration { get; protected set; }

    public static User CreateNewUser(SharedContext sharedContext,SignupDto signupDto)
    {
        switch (signupDto.DesiredRole)
        {
            case Roles.Customer:
                Customer newCustomer = new Customer(signupDto.FirstName, signupDto.LastName, signupDto.Phone,
                    signupDto.Email, signupDto.Password);
                sharedContext.Customers.Add(newCustomer);
                return newCustomer;
            case Roles.Employee:
                Employee newEmployee = new Employee(signupDto.FirstName, signupDto.LastName, signupDto.Phone,
                    signupDto.Email, signupDto.Password);
                sharedContext.Employees.Add(newEmployee);
                return newEmployee;
            case Roles.Admin:
                Admin newAdmin = new Admin(signupDto.FirstName, signupDto.LastName, signupDto.Phone, signupDto.Email,
                    signupDto.Password);
                sharedContext.Admins.Add(newAdmin);
                return newAdmin;
            default:
                throw new Exception("Invalid role");
        }
    }

    public void SetToken(string token, DateTime? tokenExpiration)
    {
        Token = token;
        TokenExpiration = tokenExpiration;
    }

    public AuthPas GetTokenAuthPas()
    {
        return new AuthPas(Token, TokenExpiration);
    }

    public void ChangeUserStandardProperties(string firstName, string lastName, string phone, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Token = "";
        TokenExpiration = null;
    }

    public abstract string GetClassName();
}
