using API.DataTransferObjects;
using API.Models.Authentication;
using API.Services.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Shared
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
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpGet("self")]
        [Authorize]
        public async Task<ActionResult<User>> GetSelf()
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                BadRequest(e.Message);
                throw;
            }
            return Ok();
        }
    }
}
