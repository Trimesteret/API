using API.DataTransferObjects;
using API.Enums;
using API.Models;
using API.Models.Authentication;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Shared;

public class UserService : IUserService
{
    private readonly SharedContext _sharedContext;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public UserService(SharedContext dbSharedContext, IAuthService authService, IMapper mapper)
    {
        _sharedContext = dbSharedContext;
        _authService = authService;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets all users in the application mapped to standard users
    /// </summary>
    /// <returns>All users as standard users</returns>
    public async Task<List<UserStandardDto>> GetAllUsers()
    {
        var users = await _sharedContext.Users.ToListAsync();
        return _mapper.Map<List<UserStandardDto>>(users);
    }

    /// <summary>
    /// Gets the currently logged in user as a standard user
    /// </summary>
    /// <returns>The currently logged in user as standard user</returns>
    public async Task<UserStandardDto> GetSelf()
    {
        var user = await _authService.GetActiveUser();
        return _mapper.Map<UserStandardDto>(user);
    }

    /// <summary>
    /// Gets a user by a given id
    /// </summary>
    /// <param name="id">The id of the user</param>
    /// <returns>User as a standard user</returns>
    public async Task<UserStandardDto> GetUserById(int id)
    {
        var user = await _sharedContext.Users.FirstOrDefaultAsync(user => user.Id == id);
        return _mapper.Map<UserStandardDto>(user);
    }

    /// <summary>
    /// Edits any user in the application
    /// </summary>
    /// <param name="userStandardDto"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<UserStandardDto> EditUser(UserStandardDto userStandardDto)
    {
        var existingUser = await _sharedContext.Users.FirstOrDefaultAsync(user => user.Id == userStandardDto.Id);

        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        existingUser.ChangeUserStandardProperties(userStandardDto.FirstName, userStandardDto.LastName, userStandardDto.Phone,
            userStandardDto.Email);

        if (userStandardDto.Role == existingUser.GetClassRoleEnum())
        {
            await _sharedContext.SaveChangesAsync();
            return _mapper.Map<UserStandardDto>(existingUser);
        }

        switch (userStandardDto.Role)
        {
            case Role.Admin:
                existingUser = new Admin(existingUser);
                break;
            case Role.Employee:
                existingUser = new Employee(existingUser);
                break;
            case Role.Customer:
                existingUser = new Customer(existingUser);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        await _sharedContext.SaveChangesAsync();

        return _mapper.Map<UserStandardDto>(existingUser);
    }

    /// <summary>
    /// Edits the currently logged in user
    /// </summary>
    /// <param name="user">The edit values</param>
    /// <returns>The edited user</returns>
    public async Task<User> EditSelf(UserStandardDto user)
    {
        var requestingUser = await _authService.GetActiveUser();

        requestingUser.ChangeUserStandardProperties(user.FirstName, user.LastName, user.Phone, user.Email);

        await _sharedContext.SaveChangesAsync();
        return requestingUser;
    }
}
