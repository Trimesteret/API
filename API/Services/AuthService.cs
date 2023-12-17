using System.Security.Claims;
using API.Models;
using API.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SharedContext _sharedContext;


        public AuthService(IHttpContextAccessor httpContextAccessor, SharedContext sharedContext)
        {
            _httpContextAccessor = httpContextAccessor;
            _sharedContext = sharedContext;
        }

        /// <summary>
        /// Gets the active user from the http context
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<User> GetActiveUser()
        {
            var userEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            if (userEmail == null)
            {
                throw new Exception("User not found");
            }

            var user = await _sharedContext.Users.FirstOrDefaultAsync(user => user.Email == userEmail);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user;
        }

        public async Task<Customer> GetActiveUserAsCustomer()
        {
            var user = await GetActiveUser();
            var customer = await _sharedContext.Customers.FirstOrDefaultAsync(customer => customer.Id == user.Id);
            return customer ?? throw new Exception("User is not a customer");
        }
    }
}
