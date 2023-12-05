using API.Models.Authentication;

namespace API.Services.Shared;

public interface IAuthService
{
    Task<User> GetActiveUser();
    Task<User> GenerateSalt();
}
