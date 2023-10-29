using API.Dtos;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authService)
        {
            _authenticationService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<AuthenticationResultDto>> Login(AuthenticationDto user)
        {
            var authenticationResult = await _authenticationService.AuthenticateUser(user);

            if (authenticationResult == null)
            {
                return BadRequest("Incorrect username or password");
            }

            return Ok(authenticationResult);
        }

        [HttpPost("LogOut")]
        public async Task<ActionResult<Boolean>> Logout([FromBody] string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is missing");
            }
            var logoutResult = await _authenticationService.LogOut(token);

           if (logoutResult)
           {
               return Ok(true);
           }

           return BadRequest("Logout failed");
        }
    }
}
