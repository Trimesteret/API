using API.DataTransferObjects;

namespace API.Services.Shared;

public interface IAuthenticationService
{
    Task<bool> CreateNewUser(SignupDto signupDto);

    Task<AuthPas> Login(LoginDto loginDto);

    Task<bool> LogOut(AuthPas authPas);

    Task<bool> VerifyToken(AuthPas authPas); }
