using API.Enums;

namespace API.Dtos;

public class AuthenticationResultDto
{
    public string Token { get; set; }
    public DateTime TokenExpiration { get; set; }

    public Roles UserRole { get; set; }
}
