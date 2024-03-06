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

        public PatientsController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] CreatePatientDto createPatientDto)
        {
            var response = await _userService.CreatePatientAsync(createPatientDto);

            return StatusCode((int)response.StatusCode, response.Message);
        }
    }
}
