using API.DataTransferObjects;
using API.Models;
using API.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Shared;

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
    /// Finds a user in the database and generates a token for them
    /// </summary>
    /// <param name="loginDto">The login data</param>
    /// <returns>An authentication pas for the users</returns>
    /// <exception cref="Exception"></exception>
    public async Task<AuthPas> Login(LoginDto loginDto)
    {
        var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Password == loginDto.Password);

        if (dbUser == null)
        {
            throw new Exception("Incorrect email or password");
        }

        var token = _tokenService.GenerateToken(dbUser);

        dbUser.SetToken(token, DateTime.Now.AddHours(24));

        await _context.SaveChangesAsync();

        return dbUser.GetTokenAuthPas();
    }

    /// <summary>
    /// Logs out a user given their token
    /// </summary>
    /// <param name="authPas">The pas with the token to logout</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> LogOut(AuthPas authPas)
    {
        var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Token == authPas.Token);

        if (dbUser == null)
        {
            throw new Exception("Failed trying to logout incorrect token");
        }

        dbUser.SetToken("", null);

        await _context.SaveChangesAsync();

        return true;
    }


    /// <summary>
    /// Verifies a user by a given token
    /// </summary>
    /// <param name="authPas">The pas with token to verify</param>
    /// <returns>Success boolean</returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> VerifyToken(AuthPas authPas)
    {
        var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Token == authPas.Token);

        if (dbUser == null)
        {
            throw new Exception("Failed trying to verify incorrect token");
        }

        if (dbUser.TokenExpiration == null || dbUser.TokenExpiration <= DateTime.Now)
        {
            dbUser.SetToken("", null);
            await _context.SaveChangesAsync();
            return false;
        }

        return true;
    }

    /// <summary>
    /// Creates a new user with a signupDto
    /// </summary>
    /// <param name="signupDto">The sign up data</param>
    /// <returns>A success boolean</returns>
    public async Task<bool> CreateNewUser(SignupDto signupDto)
    {
        User.CreateNewUser(_context, signupDto);

        await _context.SaveChangesAsync();

        return true;
    }
}
