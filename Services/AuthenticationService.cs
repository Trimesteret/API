using API.Dtos;
using API.Models;
using API.Models.Authentication;
using API.Services.Authentication;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly DBContext _context;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;

    public AuthenticationService(DBContext dbContext, IConfiguration configuration, ITokenService tokenService)
    {
        _context = dbContext;
        _configuration = configuration;
        _tokenService = tokenService;
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

    public async Task<AuthPas> Login(LoginDto user)
    {
        var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email && u.Password == user.Password);

        if (dbUser == null)
        {
            return null;
        }

        var token = _tokenService.GenerateToken(dbUser);

        var authPas = dbUser.SetToken(token, DateTime.Now.AddHours(24));

        await _context.SaveChangesAsync();

        return authPas;
    }

    /// <summary>
    /// Authenticates the user with email and password
    /// </summary>
    /// <param name="user"></param>
    /// <returns>A Token and an Expiration</returns>
    public async Task<AuthPas> AuthenticateUser(LoginDto user)
    {
        return null;
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

        dbUser.SetToken("", new DateTime());

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
            dbUser.SetToken(null, new DateTime());
            await _context.SaveChangesAsync();
            return false;
        }

        return true;
    }

    public async Task<bool> CreateNewUser(SignupDto signup)
    {
        User.CreateNewUser(_context, signup);

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<List<User>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
}
