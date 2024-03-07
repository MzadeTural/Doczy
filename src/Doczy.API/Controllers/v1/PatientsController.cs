using Doczy.Business.DTOs.RaitingDtos;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IDoctorRaitingService _dctorRaitingService;

        public PatientsController(IUserService userService, IDoctorRaitingService dctorRaitingService)
        {
            _userService = userService;
            _dctorRaitingService = dctorRaitingService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] CreatePatientDto createPatientDto)
        {
            var response = await _userService.CreatePatientAsync(createPatientDto);

            return StatusCode((int)response.StatusCode, response.Message);
        }
        [HttpPost("rating-doctor")]
        public async Task<IActionResult> RatingDoctor([FromForm] CreateRaitingDto creatRaitingtDto)
        {
            var response = await _dctorRaitingService.CreateRaitingDoctor(creatRaitingtDto);

            return StatusCode((int)response.StatusCode, response.Message);
        }
    }
}
