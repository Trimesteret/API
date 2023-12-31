using API.DataTransferObjects;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Policy = "require-admin-role")]
        public async Task<ActionResult<List<UserStandardDto>>> GetUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "require-admin-role")]
        public async Task<ActionResult<UserStandardDto>> GetUserById(int id)
        {
            return await _userService.GetUserById(id);
        }

        [HttpGet("self")]
        [Authorize]
        public async Task<ActionResult<UserStandardDto>> GetSelf()
        {
            return await _userService.GetSelf();
        }

        [HttpPut("edit")]
        [Authorize]
        public async Task<ActionResult> EditSelf(UserStandardDto user)
        {
            try
            {
                await _userService.EditSelf(user);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }


        [HttpPut("password")]
        [Authorize]
        public async Task<ActionResult> ChangeSelfPassword(LoginDto user)
        {
            try
            {
                await _userService.ChangeSelfPassword(user);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = "require-admin-role")]
        public async Task<ActionResult<UserStandardDto>> CreateUser([FromBody] UserStandardDto user)
        {
            return await _userService.CreateUser(user);
        }

        [HttpPut]
        [Authorize(Policy = "require-admin-role")]
        public async Task<ActionResult<UserStandardDto>> EditUser([FromBody] UserStandardDto user)
        {
            return await _userService.EditUser(user);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "require-admin-role")]
        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                return await _userService.DeleteUser(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
