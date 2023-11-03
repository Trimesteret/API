using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using API.Models;


namespace API.Services.Authentication;

public class TokenService : ITokenService
{
    private readonly DBContext _context;
    private readonly IConfiguration _configuration;

    public TokenService(DBContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = "b0493a0d-f88e-4b0b-94eb-665f7207c92c"u8.ToArray(); // Use the same secret key as in your JWT configuration
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Claims = new Dictionary<string, object>
            {
                { "aud", "ApiAppAudience" }, // Use the same audience name as in your JWT configuration
                { "iss", "ApiAppIssuer" }
            },
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.GetEmail()),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                // Add any other claims you want to include in the token
            }),
            Expires = DateTime.UtcNow.AddHours(24),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
