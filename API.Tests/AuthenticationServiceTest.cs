using API.DataTransferObjects;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Tests;

public class AuthenticationServiceTest
{
    /// <summary>
    /// Sign up test for new user
    /// </summary>
    [Fact]
    public async void PassSignupTest()
    {
        var context = SharedTesting.GetContext();

        var tokenService = new TokenService();
        var authenticationService = new AuthenticationService(context, tokenService);

        var signUpDto = new SignupDto
        {
            FirstName = "Test",
            LastName = "Test",
            Phone = "12345678",
            Email = "test@test.com",
            Password = "12345678",
            RepeatPassword = "12345678"
        };

        var customer = await authenticationService.SignupNewCustomer(signUpDto);
        Assert.NotNull(customer);

        var customerFromDb = await context.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);
        Assert.NotNull(customerFromDb);

        Assert.Equal(customer.Id, customerFromDb.Id);
        context.Customers.Remove(customerFromDb);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Sign up test for identical user (email) signup
    /// </summary>
    [Fact]
    public async void FailSignupTestIdenticalEmail()
    {
        var context = SharedTesting.GetContext();

        var tokenService = new TokenService();
        var authenticationService = new AuthenticationService(context, tokenService);

        var signUpDto1 = new SignupDto
        {
            FirstName = "Test",
            LastName = "Test",
            Phone = "12345678",
            Email = "test@test.com",
            Password = "12345678",
            RepeatPassword = "12345678"
        };

        var signUpDto2 = new SignupDto
        {
            FirstName = "Test",
            LastName = "Test",
            Phone = "12345678",
            Email = "test@test.com",
            Password = "12345678",
            RepeatPassword = "12345678"
        };

        var customer1 = await authenticationService.SignupNewCustomer(signUpDto1);
        Assert.NotNull(customer1);

        var customerFromDb = await context.Customers.FirstOrDefaultAsync(c => c.Id == customer1.Id);
        Assert.NotNull(customerFromDb);
        Assert.Equal(customerFromDb.Email, signUpDto1.Email);

        var exception = await Assert.ThrowsAsync<Exception>(() => authenticationService.SignupNewCustomer(signUpDto2));
        Assert.Equal("Email already exists", exception.Message);
        context.Customers.Remove(customerFromDb);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Signup test for passwords not matching
    /// </summary>
    [Fact]
    public async void FailSignupTestForPasswordsNotMatching()
    {
        var context = SharedTesting.GetContext();

        var tokenService = new TokenService();
        var authenticationService = new AuthenticationService(context, tokenService);

        var signUpDto1 = new SignupDto
        {
            FirstName = "Test",
            LastName = "Test",
            Phone = "12345678",
            Email = "test@test.com",
            Password = "12345678",
            RepeatPassword = "1234567"
        };

        var exception = await Assert.ThrowsAsync<Exception>(async () => await authenticationService.SignupNewCustomer(signUpDto1));
        Assert.Equal("Passwords do not match", exception.Message);
    }

    /// <summary>
    /// Signup test for passwords not being long enough
    /// </summary>
    [Fact]
    public async void FailSignupTestForPasswordsNotLongEnough()
    {
        var context = SharedTesting.GetContext();

        var tokenService = new TokenService();
        var authenticationService = new AuthenticationService(context, tokenService);

        var signUpDto1 = new SignupDto
        {
            FirstName = "Test",
            LastName = "Test",
            Phone = "12345678",
            Email = "test@test.com",
            Password = "123456",
            RepeatPassword = "123456"
        };

        var exception = await Assert.ThrowsAsync<Exception>(async () => await authenticationService.SignupNewCustomer(signUpDto1));
        Assert.Equal("Password must be above 7 characters", exception.Message);
    }
}