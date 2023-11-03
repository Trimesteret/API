using API.Dtos;
using API.Models;

namespace API.Services;

public interface IAuthenticationService
{
    Task<User> UserSignedUp(string email);

    Task<AuthModel> Login(AuthenticationDto user);

    Task<AuthModel> AuthenticateUser(AuthenticationDto user);

    Task<bool> LogOut(string token);

    Task<bool> VerifyToken(string token);

    Task<bool> SignupUser(string token);
}
