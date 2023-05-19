using BlazorParcelApp.Server.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorParcelApp.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet("GetMyName"), Authorize]
        public ActionResult<string> GetMe() {
            return Ok(_userService.GetUserName());
        }

        [HttpGet("GetUsernames"), Authorize]
        public async Task<ActionResult<string>> GetUsernamesAsync() {
            var result = await _userService.GetUsernames();
            return Ok(result.ToArray());
        }
    }
}
