namespace API.Dtos;

public class AuthenticationResultDto
{
    public string Token { get; set; }
    public DateTime TokenExpiration { get; set; }

    public string UserRole { get; set; }
}