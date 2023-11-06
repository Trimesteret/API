using API.Dtos;
using API.Models.Authentication;

namespace API.Models;

public abstract class User
{
    public int Id { get; protected set; }
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public int Phone { get; protected set; }
    public string Email { get; protected set; }
    public string Password { get; protected set; }
    public string Token { get; protected set; }
    public DateTime? TokenExpiration { get; protected set; }

    public static User CreateNewUser(DBContext context,SignupDto signupDto)
    {
        User newUser;

        switch (signupDto.DesiredRole)
        {
            case 1:
                newUser = new Employee(signupDto.FirstName, signupDto.LastName, signupDto.Phone, signupDto.Email,
                    signupDto.Password);
                context.Employees.Add(newUser as Employee);
                break;
            case 2:
                newUser = new Admin(signupDto.FirstName, signupDto.LastName, signupDto.Phone, signupDto.Email,
                    signupDto.Password);
                context.Admins.Add(newUser as Admin);
                break;
            default:
                newUser = new Customer(signupDto.FirstName, signupDto.LastName, signupDto.Phone, signupDto.Email,
                    signupDto.Password);
                context.Customers.Add(newUser as Customer);
                break;
        }
        return newUser;
    }

    public AuthPas SetToken(string token, DateTime tokenExpiration)
    {
        Token = token;
        TokenExpiration = tokenExpiration;
        return new AuthPas(token, tokenExpiration);
    }

    public abstract string GetClassName();
}
