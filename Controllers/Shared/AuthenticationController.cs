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

        [HttpPost]
        public async Task<ActionResult<AuthModel>> Login(AuthenticationDto user)
        {
            if(user == null)
            {
                return BadRequest("User is missing");
            }

            var authenticationResult = await _authenticationService.Login(user);

            return authenticationResult;
        }

        [HttpPost("Signup")]
        public async Task<ActionResult<AuthModel>> Signup(User user)
        {
            var authenticationResult = await _authenticationService.SignUpUser(user);

            if (authenticationResult == null)
            {
                return BadRequest("Incorrect username or password");
            }

            return Ok(authenticationResult);
        }

        [HttpPost("LogOut")]
        public async Task<ActionResult<Boolean>> Logout([FromBody] AuthModel model)
        {
            if (string.IsNullOrEmpty(model?.Token))
            {
                return BadRequest("Token is missing");
            }

            var logoutResult = await _authenticationService.LogOut(model.Token);

            if (logoutResult)
            {
                return Ok(true);
            }

            return BadRequest("Logout failed");
        }

        [HttpPost("Verify")]
        public async Task<ActionResult<Boolean>> VerifyToken([FromBody] AuthModel model)
        {
            if (string.IsNullOrEmpty(model?.Token))
            {
                return BadRequest("Token is missing");
            }

            var verified = await _authenticationService.VerifyToken(model.Token);

            if (verified)
            {
                return Ok(true);
            }

            return BadRequest("Verification failed");
        }
    }
}
