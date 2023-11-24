using API.DataTransferObjects;
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

        [HttpPost("Verify")]
        public async Task<ActionResult<Boolean>> VerifyToken([FromBody] AuthPas authPas)
        {
            try
            {
                return Ok(await _authenticationService.VerifyToken(authPas));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Ok(false);
            }
        }
    }
}
