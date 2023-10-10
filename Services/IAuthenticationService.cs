using API.Dtos;
using API.Models.Authentication;

namespace NewAPI.Services;

public interface IAuthenticationService
{
    Task<User> UserSignedUp(String email);

    Task<AuthenticationResultDto> AuthenticateUser(AuthenticationDto user);
}