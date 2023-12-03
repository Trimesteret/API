using System.Diagnostics;
using System.Security.Cryptography;
using API.DataTransferObjects;
using API.Enums;
using API.Models;
using API.Models.Authentication;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Shared;

public class UserService : IUserService
{
    private readonly SharedContext _sharedContext;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _authenticationService;

    const int KeySize = 64;

    public UserService(SharedContext dbSharedContext, IAuthService authService, IMapper mapper, IAuthenticationService authenticationService)
    {
        _sharedContext = dbSharedContext;
        _authService = authService;
        _mapper = mapper;
        _authenticationService = authenticationService;
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

        Console.WriteLine(userStandardDto.Role == existingUser.Role);
        if (userStandardDto.Role == existingUser.Role)
        {
            await _sharedContext.SaveChangesAsync();
            return _mapper.Map<UserStandardDto>(existingUser);
        }

        _sharedContext.Users.Remove(existingUser);
        await _sharedContext.SaveChangesAsync();


        switch (userStandardDto.Role)
        {
            case Role.Admin:
                Admin admin = new Admin(existingUser);
                await _sharedContext.Users.AddAsync(admin);
                await _sharedContext.SaveChangesAsync();
                return _mapper.Map<UserStandardDto>(admin);

            case Role.Employee:
                Employee employee = new Employee(existingUser);
                await _sharedContext.Employees.AddAsync(employee);
                await _sharedContext.SaveChangesAsync();
                return _mapper.Map<UserStandardDto>(employee);

            case Role.Customer:
                Customer customer = new Customer(existingUser);
                await _sharedContext.Customers.AddAsync(customer);
                await _sharedContext.SaveChangesAsync();
                return _mapper.Map<UserStandardDto>(customer);

            default:
                throw new ArgumentOutOfRangeException();
        }
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

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<bool> DeleteUser(int id)
    {
        var existingUser = _sharedContext.Users.FirstOrDefault(user => user.Id == id);
        if (existingUser == null)
        {
            throw new Exception("Could not find user with id: " + id);
        }
        _sharedContext.Users.Remove(existingUser);
        await _sharedContext.SaveChangesAsync();
        return true;
    }
    public async Task<User> ChangeSelfPassword(LoginDto loginDto)
    {
        //get user
        var dbUser = await _sharedContext.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);

        //Returns without changes, if user has not requested any password changes
        if (loginDto.Password == null)
        {
            return dbUser;
        }
        //verify current password
        if (AuthenticationService.HashPassword(loginDto.Password, dbUser.Salt) != dbUser.Password)
        {
            throw new Exception("Incorrect password");
        }

        //check that new passwords are the same
        if (loginDto.NewPasswordOne != loginDto.NewPasswordTwo)
        {
            throw new Exception("Passwords don't match");
        }

        //save changes to db
        var salt = RandomNumberGenerator.GetBytes(KeySize);
        var hashedPassword = AuthenticationService.HashPassword(loginDto.NewPasswordOne, salt);
        dbUser.ChangeUserPassword(hashedPassword, salt);
        await _sharedContext.SaveChangesAsync();

        return dbUser;
    }
}

