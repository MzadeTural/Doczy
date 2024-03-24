using Doczy.Business.DTOs.DoctorAvailabilityDtos;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilitiesController : ControllerBase
    {
        private readonly IDoctorAvailabilityService _doctorAvailabilityService;

        public AvailabilitiesController(IDoctorAvailabilityService doctorAvailabilityService)
        {
            _doctorAvailabilityService = doctorAvailabilityService;
        }

        [HttpPost("")]
        public async Task<IActionResult> CreateDoctorAvailability(CreateDoctorAvailabilityDto createDto)
        {
            var response = await _doctorAvailabilityService.CreateDoctorAvailabilityAsync(createDto);
            return StatusCode((int)response.StatusCode, response.Message);
        }
        [HttpGet("")]
        public async Task<List<GetDoctorAvailabilityDto>> GetDoctorAvailability()
        {
            var response = await _doctorAvailabilityService.GetDoctorOwnAvailabilityAsync();
            return response;

        }
        [HttpGet("{doctorId}/{date}")]
        public async Task<GetDoctorAvailabilityDto> GetDoctorAvailability([FromRoute] Guid doctorId, [FromRoute] DateTime date)
        {
            var response = await _doctorAvailabilityService.GetDoctorAvailabilityAsync(doctorId, date);
            return response;

        }
    }
}
