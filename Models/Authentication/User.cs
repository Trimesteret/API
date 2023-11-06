namespace API.Models;

public abstract class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string? Token { get; set; }
    public DateTime? TokenExpiration { get; set; }
}
