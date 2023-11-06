using API.Dtos;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Shared
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

        [HttpPost("Login")]
        public async Task<ActionResult<AuthPas>> Login(AuthenticationDto user)
        {
            if(user == null)
            {
                return BadRequest("User is missing");
            }

            var authenticationResult = await _authenticationService.Login(user);

            return authenticationResult;
        }

        [HttpPost("Signup")]
        public async Task<ActionResult<AuthPas>> Signup(UserDto user)
        {
            var authenticationResult = await _authenticationService.CreateNewUser(user);

            if (authenticationResult == null)
            {
                return BadRequest("Incorrect username or password");
            }

            return Ok(authenticationResult);

        }

        [HttpPost("LogOut")]
        public async Task<ActionResult<Boolean>> Logout([FromBody] AuthPas pas)
        {
            if (string.IsNullOrEmpty(pas?.Token))
            {
                return BadRequest("Token is missing");
            }

            var logoutResult = await _authenticationService.LogOut(pas.Token);

            if (logoutResult)
            {
                return Ok(true);
            }

            return BadRequest("Logout failed");
        }

        [HttpPost("Verify")]
        public async Task<ActionResult<Boolean>> VerifyToken([FromBody] AuthPas pas)
        {
            if (string.IsNullOrEmpty(pas?.Token))
            {
                return BadRequest("Token is missing");
            }

            var verified = await _authenticationService.VerifyToken(pas.Token);

            if (verified)
            {
                return Ok(true);
            }

            return BadRequest("Verification failed");
        }
    }
}
