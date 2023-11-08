using API.Models.Authentication;

namespace API.Services.Shared;

public interface ITokenService
{
    public string GenerateToken(User user);
}
