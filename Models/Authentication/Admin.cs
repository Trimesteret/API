using API.Enums;
using API.Models.Items;

namespace API.Models.Authentication;

public class Admin : Employee
{
    public Admin(string firstName, string lastName, string phone, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Token = "";
        Role = Role.Admin;
    }

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
    }

    protected void ChangeAdmin(string firstName, string lastName, string phone, string email, string password, int? phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Token = "";
        Role = Role.Admin;
    }

    public void EditUser(User user, string firstName, string lastName, int phone, string email, string password, int? phoneNumber)
    {
        Console.WriteLine(user.GetType());
    }

    public Item CreateItem(string name, string ean, int quantity, float price, string imageUrl)
    {
        return new DefaultItem(name, ean, quantity, price, imageUrl);
    }
}
