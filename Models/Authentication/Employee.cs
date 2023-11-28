using API.Enums;

namespace API.Models.Authentication;

public class Employee : User
{
    public Employee(){}

    public Employee(string firstName, string lastName, string phone, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Token = "";
        Role = Role.Employee;
    }

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
    }

    protected void ChangeEmployee(string firstName, string lastName, string phone, string email, string password, int? phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Token = "";
    }
}
