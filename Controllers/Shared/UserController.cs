using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly IAuthenticationService _authenticationService;

        public UserController(IAuthenticationService authService)
        {
            _authenticationService = authService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            var users = await _authenticationService.GetUsers();

            return users;
        }
    }
}
