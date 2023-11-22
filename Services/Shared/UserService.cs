using API.DataTransferObjects;
using API.Models;
using API.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Shared;

public class UserService : IUserService
{
    private readonly Context _context;
    private readonly IAuthService _authService;

    public UserService(Context dbContext, IAuthService authService)
    {
        _context = dbContext;
        _authService = authService;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetSelf()
    {
        return await _authService.GetActiveUser();
    }

    public Task<ActionResult> EditUser(UserStandardDto user)
    {
        throw new NotImplementedException();
    }

    public async Task<User> EditSelf(UserStandardDto user)
    {
        var requestingUser = await _authService.GetActiveUser();

        requestingUser.ChangeUserStandardProperties(user.FirstName, user.LastName, user.Phone, user.Email, user.Password);

        await _context.SaveChangesAsync();
        return requestingUser;
    }
}
