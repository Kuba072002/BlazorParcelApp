using BlazorParcelApp.Server.Services.LokcerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BlazorParcelApp.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class LockerController : ControllerBase {

        private readonly ILockerService _lockerService;

        public LockerController(ILockerService lockerService) {
            _lockerService = lockerService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,User")]
        public async Task<ActionResult<List<LockerDto>>> GetLockers() {
            var response = await _lockerService.GetLockers();
            if (!response.Success) {
                return BadRequest(response);
            }
            return Ok(response.Data.ToArray());
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<LockerDto>> AddLocker(LockerDto lockerDto) {
            var response = await _lockerService.AddLocker(lockerDto);
            if (!response.Success) {
                return BadRequest(response);
            }
            return Ok(response.Data);
        }

        [HttpPut,Authorize(Roles = "Admin")]
        public async Task<ActionResult<string>> UpdateLocker(LockerDto lockerDto)
        {
            var response = await _lockerService.UpdateLocker(lockerDto);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }

        [HttpDelete("{lockerId}"),Authorize(Roles = "Admin")]
        public async Task<ActionResult<string>> DeleteLocker(int lockerId)
        {
            var response = await _lockerService.DeleteLocker(lockerId);
            if (!response.Success)
            {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }
    }
}
