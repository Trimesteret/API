namespace API.DataTransferObjects;

public class AuthPas
{
    public string Token { get; set; }
    public DateTime? ExpirationDate { get; set; }

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
