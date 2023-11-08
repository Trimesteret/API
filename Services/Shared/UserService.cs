using API.Models;
using API.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Shared;

public class UserService : IUserService
{
    private readonly DBContext _context;

    public UserService(DBContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<List<User>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }
}
