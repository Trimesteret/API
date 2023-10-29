using API.Dtos;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Services;

public interface IAuthenticationService
{
    Task<User> UserSignedUp(String email);

    Task<AuthenticationResultDto> AuthenticateUser(AuthenticationDto user);

    Task<bool> LogOut(String token);
}
