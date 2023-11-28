using API.DataTransferObjects;
using API.Enums;
using API.Services.Shared;
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
        public async Task<ActionResult<AuthPas>> Login(LoginDto loginDto)
        {
            try
            {
                var authPas = await _authenticationService.Login(loginDto);
                return authPas;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Signup")]
        public async Task<ActionResult<AuthPas>> Signup(SignupDto signupDto)
        {
            try
            {
                await _authenticationService.SignupNewCustomer(signupDto);
                return Ok(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("LogOut")]
        public async Task<ActionResult<Boolean>> Logout([FromBody] AuthPas authPas)
        {
            try
            {
                return Ok(await _authenticationService.LogOut(authPas));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Verify")]
        public async Task<ActionResult<Boolean>> VerifyToken([FromQuery] string token)
        {
            try
            {
                return Ok(await _authenticationService.VerifyToken(token));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(false);
            }
        }

        [HttpGet("VerifyRole")]
        public async Task<ActionResult<Roles>> VerifyRole([FromQuery] string token, [FromQuery] Roles role)
        {
            try
            {
                return Ok(await _authenticationService.VerifyRole(token, role));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(false);
            }
        }
    }
}
