using API.Dtos;

namespace API.Models;

public abstract class User
{
    public int Id { get; protected set; }
    public string FirstName { get; protected set; }
    public string LastName { get; protected set; }
    public int Phone { get; protected set; }
    public string Email { get; protected set; }
    public string Password { get; protected set; }
    public string Token { get; protected set; }
    public DateTime? TokenExpiration { get; protected set; }

    public AuthPas SetToken(DBContext dbContext,string token, DateTime tokenExpiration)
    {
        Token = token;
        TokenExpiration = tokenExpiration;
        return new AuthPas(token, tokenExpiration);
    }

    public abstract string GetClassName();
}
