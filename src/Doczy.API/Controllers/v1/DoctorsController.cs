using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.Experiance;
using Doczy.Business.DTOs.Language;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IDoctorService _doctorService;
        private readonly IExperianceService _experianceService;
        public DoctorsController(IUserService userService, IDoctorService doctorService, IExperianceService experianceService)
        {

            _userService = userService;
            _doctorService = doctorService;
            _experianceService = experianceService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] CreateDoctorDto createDoctorDto)
        {
            var response = await _userService.CreateDoctorAsync(createDoctorDto);

            return StatusCode((int)response.StatusCode, response.Message);
        }
        [HttpGet("/doctor-appointments")]
        public async Task<List<GetDoctorAppointmentsDto>> GetDoctorAppointments()
        {
            var appointments = await _doctorService.GetDoctorAppointments();
            return appointments;
        }


        [HttpPatch("/phone")]
        public async Task<IActionResult> UpdatePhoneNumber([FromForm] UserPhoneUpdateDto model)
        {
            var response = await _doctorService.UpdatePhoneNumberAsync(model);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }



        [Authorize]
        [HttpPatch("{languageId}/language")]
        public async Task<IActionResult> AddLanguage(Guid languageId)
        {
            var response = await _doctorService.AddLanguageAsync(languageId);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }

        [HttpPost("experiance")]
        public async Task<IActionResult> AddExperiance([FromForm] CreateExperianceDto createExperianceDto)
        {
            var response = await _experianceService.CreateExperianceAsync(createExperianceDto);
            return StatusCode((int)response.StatusCode, response.Message);
        }
        
        [HttpGet("/user-languages")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        public async Task<List<GetLanguageDto>> GetDoctorLanguages()
        {
            var languages = await _doctorService.GetLanguageAsync();
            return languages;
        }


    }
}
