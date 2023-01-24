using BlazorParcelApp.Server.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    }
}
