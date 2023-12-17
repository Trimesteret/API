using API.Models.Authentication;

namespace API.Services;

public interface ITokenService
{
    public string GenerateToken(User user);
}
