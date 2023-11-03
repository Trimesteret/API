using API.Enums;

namespace API.Dtos;

public class AuthModel
{
    public string Token { get; set; }
    public Roles? UserRole { get; set; }
}
