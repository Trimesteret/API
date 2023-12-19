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
    public string? Password { get; protected set; }
    public Byte[]? Salt { get; protected set; }
    public string? Token { get; protected set; }
    public bool SignedUp { get; protected set; } = false;
    public DateTime? TokenExpiration { get; protected set; }
    public Role Role { get; protected set; }

    public void SetToken(string token, DateTime? tokenExpiration)
    {
        Token = token;
        TokenExpiration = tokenExpiration;
    }

    public AuthPas GetTokenAuthPas()
    {
        return new AuthPas(Token, TokenExpiration, Role);
    }

    public void ChangeUserStandardProperties(string firstName, string lastName, string phone, string email)
    {
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
        Email = email;
        Token = null;
        TokenExpiration = null;
    }

    public void ChangePassword(string password, Byte[] salt)
    {
        this.Password = password;
        this.Salt = salt;
    }

    public void SetSignedUp(bool signedUp)
    {
        if(signedUp && Password == null)
        {
            return;
        }

        SignedUp = signedUp;
    }
}
