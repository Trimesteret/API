using System.Security.Claims;
using API.Models;
using API.Models.Authentication;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Shared
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DBContext _context;


        public AuthService(IHttpContextAccessor httpContextAccessor, DBContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<User> GetActiveUser()
        {
            var userEmail = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email)?.Value;
            if (userEmail == null)
            {
                throw new Exception("User not found");
            }

            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == userEmail);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            return user;
        }
    }
}
