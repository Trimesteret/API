using API.DataTransferObjects;
using API.Models;
using API.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Shared;

public class UserService : IUserService
{
    private readonly DBContext _context;
    private readonly IAuthService _authService;

    public UserService(DBContext dbContext, IAuthService authService)
    {
        _context = dbContext;
        _authService = authService;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<ActionResult> EditUser(UserStandardDto user)
    {
        var requestingUser = await _authService.GetActiveUser();
        if (requestingUser.Email != user.Email)
        {
            throw new Exception("You can only edit your own user");
        }

        Console.WriteLine(user.GetType());
        Console.WriteLine(requestingUser.GetType());
        throw new NotImplementedException();
    }
}
