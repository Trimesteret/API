using API.DataTransferObjects;
using API.Enums;
using API.Models.Authentication;

namespace API.Services.Shared;

public interface IAuthenticationService
{
    Task<Customer> SignupNewCustomer(SignupDto signupDto);

    Task<AuthPas> Login(LoginDto loginDto);

    Task<bool> LogOut(AuthPas authPas);

    Task<bool> VerifyToken(string token);

    Task<Role> VerifyRole(string token, Role role);
}
