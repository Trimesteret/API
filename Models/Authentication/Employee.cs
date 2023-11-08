namespace API.Models.Authentication;

public class Employee : User
{
    public Employee(){}

    public Employee(string firstName, string lastName, int phone, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Token = "";
    }

    public Employee(User user)
    {
        FirstName = user.FirstName;
        LastName = user.LastName;
        Phone = user.Phone;
        Email = user.Email;
        Password = user.Password;
        Token = "";
    }

    override
    public string GetClassName()
    {
        return "Employee";
    }
}
