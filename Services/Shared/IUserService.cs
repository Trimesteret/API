using API.DataTransferObjects;
using API.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Shared;

public interface IUserService
{
    public Task<List<UserStandardDto>> GetAllUsers();

    public Task<UserStandardDto> GetSelf();

    public Task<UserStandardDto> GetUserById(int id);

    public Task<UserStandardDto> EditUser(UserStandardDto userStandardDto);

    public Task<User> EditSelf(UserStandardDto user);

    public Task ChangeSelfPassword(LoginDto user);

    public Task<bool> DeleteUser(int id);
}
