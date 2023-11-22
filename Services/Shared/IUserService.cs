using API.DataTransferObjects;
using API.Models.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Shared;

public interface IUserService
{
    public Task<List<User>> GetAllUsers();

    public Task<User> GetSelf();

    public Task<ActionResult> EditUser(UserStandardDto user);

    public Task<User> EditSelf(UserStandardDto user);
}
