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

    public async Task<AuthModel> Login(AuthenticationDto user)
    {
        var dbUser = User.LoginUser(_context, user.Email, user.Password);

        if (dbUser == null)
        {
            return null;
        }

        var token = GenerateToken(dbUser);
        var tokenExpiration = DateTime.UtcNow.AddHours(24);

        dbUser.Token = token;
        dbUser.TokenExpiration = tokenExpiration;

        await _context.SaveChangesAsync();

        return new AuthModel
        {
            Token = token,
            UserRole = dbUser.Role
        };
    }

    /// <summary>
    /// Authenticates the user with email and password
    /// </summary>
    /// <param name="user"></param>
    /// <returns>A Token and an Expiration</returns>
    public async Task<AuthModel> AuthenticateUser(AuthenticationDto user)
    {

    }

    /// <summary>
    /// Log outs the user
    /// </summary>
    /// <param name="token">The token of the user</param>
    /// <returns></returns>
    public async Task<bool> LogOut(string token)
    {
        var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Token == token);

        if (dbUser == null)
        {
            return false;
        }

        dbUser.Token = null;
        dbUser.TokenExpiration = null;

        await _context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Verifies a given token
    /// </summary>
    /// <param name="token">Token to verify</param>
    /// <returns></returns>
    public async Task<bool> VerifyToken(string token)
    {
        var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Token == token);

        if (dbUser == null)
        {
            return false;
        }

        if (dbUser.TokenExpiration == null || dbUser.TokenExpiration <= DateTime.Now)
        {
            dbUser.Token = null;
            dbUser.TokenExpiration = null;
            await _context.SaveChangesAsync();
            return false;
        }

        return true;
    }

    public Task<bool> SignupUser(string token)
    {
        throw new NotImplementedException();
    }

    /**
     * Generates a token
     */

}
