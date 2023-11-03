using API.Enums;

namespace API.Models;

public abstract class User
{
    private int? Id;
    private string Name;
    private int Phone;
    private string Email;
    public string Password;
    public Roles? Role;
    public string? Token;
    public DateTime? TokenExpiration;

    public static List<User> Users = new List<User>();

    public int GetId()
    {
        return this.Id;
    }

    public void SetToken(string token)
    {
        this.Token = token;
    }

    public string GetToken()
    {
        return this.Token;
    }

    public void SetEmail(string email)
    {
        this.Email = email;
    }

    public string GetEmail()
    {
        return this.Email;
    }

    public User(string name, int phone, string email, string password, Roles role)
    {
        this.Name = name;
        this.Phone = phone;
        this.Email = email;
        this.Password = password;
        this.Role = role;
    }

    public static List<User> GetUsers(DBContext context)
    {
        return context.Users.ToList();
    }

    public static User? GetUserById(DBContext context, int id)
    {
        return context.Users.FirstOrDefault(u => u.Id == id);
    }

    public static User? LoginUser(DBContext context, string email, string password)
    {
        return context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
    }
}
