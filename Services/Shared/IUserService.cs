using API.Models.Authentication;

namespace API.Services.Shared;

public interface IUserService
{
    public Task<List<User>> GetAllUsers();
}
