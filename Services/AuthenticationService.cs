using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using API.Dtos;
using API.Models;

namespace API.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly DBContext _context;
    private readonly IConfiguration _configuration;

    public AuthenticationService(DBContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    /// <summary>
    /// Checks if email is signed up
    /// </summary>
    /// <param name="email">Email to check</param>
    /// <returns>User if email is signed up or null</returns>
    public async Task<User> UserSignedUp(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    /// <summary>
    /// Authenticates the user with email and password
    /// </summary>
    /// <param name="user"></param>
    /// <returns>A Token and an Expiration</returns>
    public async Task<AuthenticationResultDto> AuthenticateUser(AuthenticationDto user)
    {
        var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

        if (dbUser == null)
        {
            return null;
        }

        var token = GenerateToken(dbUser);
        var tokenExpiration = DateTime.UtcNow.AddHours(24);

        dbUser.Token = token;
        dbUser.TokenExpiration = tokenExpiration;

        await _context.SaveChangesAsync();

        return new AuthenticationResultDto
        {
            Token = token,
            TokenExpiration = tokenExpiration,
            UserRole = dbUser.Role
        };
    }

    /**
     * Generates a token
     */
    private string GenerateToken(User user)
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
                new Claim(ClaimTypes.Email, user.Email),
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
