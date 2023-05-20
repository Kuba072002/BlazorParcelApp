using BlazorParcelApp.Server.Services.ParcelService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorParcelApp.Server.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ParcelController : ControllerBase {
        private readonly IParcelService _parcelService;

        public ParcelController(IParcelService parcelService) {
            _parcelService = parcelService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ParcelDto>>> GetParcels() {
            var response = await _parcelService.GetParcels();
            if (!response.Success) {
                return BadRequest(response);
            }
            var result = response.Data.ToArray();
            return Ok(result);
        }

        [HttpGet("GetParcelsByUser/{username}")]
        public async Task<ActionResult<List<ParcelDto>>> GetParcelsByUser(string username)
        {
            var response = await _parcelService.GetParcelsByUser(username);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            var result = response.Data.ToArray();
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<ParcelDto>> GetParcel(int Id) {
            var response = await _parcelService.GetParcel(Id);
            if (!response.Success) {
                return NotFound(response);
            }
            return Ok(response.Data);
        }
        [HttpPost]
        public async Task<ActionResult<string>> AddParcel(ParcelDto parcelDto) {
            var response = await _parcelService.AddParcel(parcelDto);
            if (!response.Success) {
                return BadRequest(response.Message);
            }
            return Ok(response.Message);
        }
        [HttpPut("UpdateParcelState/{parcelId}")]
        public async Task<ActionResult<ParcelDto>> UpdateParcelState(int parcelId) {
            var response = await _parcelService.UpdateParcelState(parcelId);
            if (!response.Success) {
                return BadRequest(response);
            }
            return Ok(response.Data);
        }
    }
}
