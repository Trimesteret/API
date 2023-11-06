namespace API.Dtos;

public class AuthPas
{
    public string Token { get; protected set; }
    public DateTime ExpirationDate { get; protected set; }

    public AuthPas(string token, DateTime? expirationDate)
    {
        Token = token;

        if (expirationDate != null)
        {
            ExpirationDate = expirationDate.Value;
        }

        ExpirationDate = DateTime.Now.AddHours(24);
    }
}
