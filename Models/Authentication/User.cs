namespace API.Models.Authentication;

public class User
{
    public Int32? Id { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }
    public string? Role { get; set; }
    public string? Token { get; set; }
    public DateTime? TokenExpiration { get; set; }
}