using API.DataTransferObjects;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Tests;

public class LoginTest
{
    [Fact]
    public async void PassLoginTest()
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
        Assert.NotNull(customer.Password);

        var loginDto = new LoginDto
        {
            Email = customer.Email,
            Password = signUpDto.Password
        };

        var loginRes = await authenticationService.Login(loginDto);
        Assert.NotNull(loginRes);

        var customerFromDb = await context.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);
        Assert.NotNull(customerFromDb);

        Assert.Equal(loginRes.Token, customerFromDb.Token);
        Assert.NotEqual(loginRes.ExpirationDate, customerFromDb.TokenExpiration);
        Assert.Equal(loginRes.Role, customerFromDb.Role);

        context.Users.Remove(customerFromDb);
        await context.SaveChangesAsync();
    }
}
