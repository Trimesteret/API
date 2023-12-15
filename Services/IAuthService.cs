using API.Models.Authentication;

namespace API.Services;

public interface IAuthService
{
    Task<User> GetActiveUser();
}
