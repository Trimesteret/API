using API.Dtos;
using API.Models;

namespace API.Services;

public interface IAuthenticationService
{
    Task<bool> CreateNewUser(UserDto user);

    Task<AuthPas> Login(AuthenticationDto user);

    Task<AuthPas> AuthenticateUser(AuthenticationDto user);

    Task<bool> LogOut(string token);

    Task<bool> VerifyToken(string token);

    Task<List<User>> GetUsers();
}
