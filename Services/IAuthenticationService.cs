using API.Dtos;
using API.Models;

namespace API.Services;

public interface IAuthenticationService
{
    Task<bool> CreateNewUser(SignupDto signup);

    Task<AuthPas> Login(LoginDto user);

    Task<AuthPas> AuthenticateUser(LoginDto user);

    Task<bool> LogOut(string token);

    Task<bool> VerifyToken(string token);

    Task<List<User>> GetUsers();
}
