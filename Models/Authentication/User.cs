using API.Enums;

namespace API.Models;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Roles Role { get; set; }
    public string? Token { get; set; }
    public DateTime? TokenExpiration { get; set; }
}
