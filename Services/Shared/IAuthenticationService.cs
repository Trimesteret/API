using API.DataTransferObjects;
using API.Models.Authentication;

namespace API.Services.Shared;

public interface IAuthenticationService
{
    Task<Customer> SignupNewCustomer(SignupDto signupDto);

    Task<AuthPas> Login(LoginDto loginDto);

    Task<bool> LogOut(AuthPas authPas);

    Task<bool> VerifyToken(AuthPas authPas);
}
