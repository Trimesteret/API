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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<User>>> GetUsers()
        {
            return await _userService.GetAllUsers();
        }

        [HttpPost("/edit")]
        [Authorize]
        public async Task<ActionResult> EditSelf(UserStandardDto user)
        {
            try
            {
                await _userService.EditUser(user);
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
