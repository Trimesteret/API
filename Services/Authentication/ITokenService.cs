using API.Models;

namespace API.Services.Authentication;

public interface ITokenService
{
    public string GenerateToken(User user);
}
