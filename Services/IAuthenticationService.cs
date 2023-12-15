using API.DataTransferObjects;
using API.Enums;
using API.Models.Authentication;

namespace API.Services;

public interface IAuthenticationService
{
    Task<Customer> SignupNewCustomer(SignupDto signupDto);

    Task<AuthPas> Login(LoginDto loginDto);

    Task<string> ForgotPassword(ForgotPasswordDto forgotPasswordDto);

    Task<bool> LogOut(AuthPas authPas);

    Task<bool> VerifyToken(string token);

    Task<Role> VerifyRole(string token, Role expectedRole);
}
