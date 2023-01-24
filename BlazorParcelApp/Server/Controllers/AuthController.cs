using BlazorParcelApp.Server.Services;
using BlazorParcelApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorParcelApp.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request) {
            var response = await _authService.Register(
                new User {
                    Username= request.Username,
                    Email = request.Email
                },
                request.Password);

            if (!response.Success) {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(UserLoginDto request) {
            var response = await _authService.Login(request.Email, request.Password);
            if (!response.Success) {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
