using API.Models.Items;

namespace API.Models.Authentication;

public class Admin : Employee
{
    public Admin(string firstName, string lastName, int phone, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Token = "";
    }

    public Admin(User user)
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
        return "Admin";
    }

    public void EditUser(User user, string firstName, string lastName, int phone, string email, string password, int? phoneNumber)
    {
        user.ChangeUser(firstName, lastName, phone, email, password, phoneNumber);
    }

    public Item CreateItem(string name, string ean, int quantity, float price, string imageUrl, DateTime expirationDate)
    {
        return new DefaultItem(name, ean, quantity, price, imageUrl, expirationDate);
    }
}
