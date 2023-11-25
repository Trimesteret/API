using API.DataTransferObjects;
using API.Enums;

namespace API.Models.Authentication;

public abstract class User
{
    public int Id { get; protected set; }
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public string Phone { get; protected set; }
    public string Email { get; protected set; }
    public string Password { get; protected set; }
    public Byte[] Salt { get; protected set; }
    public string Token { get; protected set; }
    public DateTime? TokenExpiration { get; protected set; }

    public void SetToken(string token, DateTime? tokenExpiration)
    {
        Token = token;
        TokenExpiration = tokenExpiration;
    }

    public AuthPas GetTokenAuthPas()
    {
        return new AuthPas(Token, TokenExpiration);
    }

    public void ChangeUserStandardProperties(string firstName, string lastName, string phone, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Password = password;
        Token = "";
        TokenExpiration = null;
    }

    public abstract string GetClassName();
}
