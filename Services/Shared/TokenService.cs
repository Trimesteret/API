using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using API.Models.Authentication;


namespace API.Services.Shared;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Generates a token for a given user
    /// </summary>
    /// <param name="user">The user to generate a token for</param>
    /// <returns>Token as a string</returns>
    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = "b0493a0d-f88e-4b0b-94eb-665f7207c92c"u8.ToArray();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Claims = new Dictionary<string, object>
            {
                { "aud", "ApiAppAudience" },
                { "iss", "ApiAppIssuer" }
            },
            Subject = new ClaimsIdentity(new Claim[]
            {
                new (ClaimTypes.Email, user.Email),
                new (ClaimTypes.Role, user.GetClassName()),
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
