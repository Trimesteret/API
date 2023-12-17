using API.Enums;

namespace API.DataTransferObjects;

public class AuthPas
{
    public string? Token { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public Role? Role { get; set; }

    public AuthPas(string token, DateTime? expirationDate, Role? role)
    {
        Token = token;

        if (expirationDate != null)
        {
            ExpirationDate = expirationDate.Value;
        }

        if(role != null)
        {
            Role = role.Value;
        }

        ExpirationDate = DateTime.Now.AddHours(24);
    }
}
