using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Doczy.Business.Services.Interfaces;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.Common;
using System.Net;
using Doczy.Business.DTOs.ServiceDtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IDoctorService _doctorService;
        public DoctorsController(IUserService userService, IDoctorService doctorService)
        {

            _userService = userService;
            _doctorService = doctorService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] CreateDoctorDto createDoctorDto)
        {
            var response = await _userService.CreateDoctorAsync(createDoctorDto);

            return StatusCode((int)response.StatusCode, response.Message);
        }
        [HttpGet("{userId}/doctor-appointments")]
        public async Task<List<GetDoctorAppointmentsDto>> GetDoctorAppointments(Guid userId)
        {
            var appointments = await _doctorService.GetDoctorAppointments(userId);
            return appointments;
        }

        
        [HttpPatch("{userId}/phone")]
        public async Task<IActionResult> UpdatePhoneNumber(Guid userId, [FromForm] UserPhoneUpdateDto model)
        {
            var response = await _doctorService.UpdatePhoneNumberAsync(userId, model);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpPost("createUserWithCars")]
       
            [HttpPatch("{userId},{workPlaceId}/workplace")]
        public async Task<IActionResult> UpdateWorkPlace(Guid userId,Guid workPlaceId, [FromForm] UserPhoneUpdateDto model)
        {
            var response = await _doctorService.UpdateWorkPlaceAsync(userId, workPlaceId);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }


    }
}
