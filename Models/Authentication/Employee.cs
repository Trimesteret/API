using Microsoft.EntityFrameworkCore;

namespace API.Models.Authentication;

public class Employee : User
{
    private readonly DBContext _context;

    public Employee(int id, string firstName, string lastName, int phone, string email, string password, string token, DateTime? tokenExpiration)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Token = token;
        TokenExpiration = tokenExpiration;
    }

    override
    public string GetClassName()
    {
        return "Employee";
    }
}
