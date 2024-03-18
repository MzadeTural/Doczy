using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorAvailabilityDtos;
using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.Experiance;
using Doczy.Business.DTOs.Language;
using Doczy.Business.DTOs.ServiceDtos;
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
    //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
    public class DoctorsController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IDoctorService _doctorService;
        private readonly IExperianceService _experianceService;
        private readonly IDoctorAvailabilityService _doctorAvailabilityService;
        private readonly IServiceService _serviceService;
        public DoctorsController(IUserService userService, IDoctorService doctorService, IExperianceService experianceService, IDoctorAvailabilityService doctorAvailabilityService, IServiceService serviceService)
        {

            _userService = userService;
            _doctorService = doctorService;
            _experianceService = experianceService;
            _doctorAvailabilityService = doctorAvailabilityService;
            _serviceService = serviceService;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] CreateDoctorDto createDoctorDto)
        {
            var response = await _userService.CreateDoctorAsync(createDoctorDto);

            return StatusCode((int)response.StatusCode, response.Message);
        }

        [HttpGet("")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllDoctors()
        {
            return Ok(await _doctorService.GetDoctors());
        }
        [HttpGet("appointments")]
        public async Task<List<GetDoctorAppointmentsDto>> GetDoctorAppointments()
        {
            var appointments = await _doctorService.GetDoctorAppointments();
            return appointments;
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<GetDoctorsDto>>> FilterDoctors([FromQuery] GetDoctorFilterDto model)
        {
            var response = await _doctorService.GetFilterDoctors(model);
            return Ok(response);

        }


        [HttpPatch("update-phone")]
        public async Task<IActionResult> UpdatePhoneNumber([FromForm] UserPhoneUpdateDto model)
        {
            var response = await _doctorService.UpdatePhoneNumberAsync(model);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpPost("add-language/{languageId}")]
        public async Task<IActionResult> AddLanguage(Guid languageId)
        {
            var response = await _doctorService.AddLanguageAsync(languageId);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpPatch("add-category/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId)
        {
            var response = await _doctorService.UpdateCategoryAsync(categoryId);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }

        

        [HttpPost("create-doctor-availability")]
        public async Task<IActionResult> CreateDoctorAvailability( CreateDoctorAvailabilityDto createDto)
        {
            var response = await _doctorAvailabilityService.CreateDoctorAvailabilityAsync(createDto);
            return StatusCode((int)response.StatusCode, response.Message);
        }

        
        [HttpGet("languages")]
        public async Task<List<GetLanguageDto>> GetDoctorLanguages()
        {
            var languages = await _doctorService.GetLanguageAsync();
            return languages;
        }
        [HttpGet("availabilities")]
        public async Task<GetDoctorAvailabilityDto> GetDoctorAvailability(Guid id ,DateTime date)
        {
            var response = await _doctorAvailabilityService.GetDoctorAvailabilityAsync(id,date);
            return response;

        }
        [HttpGet("resume/{id}")]
        public async Task<GetDoctorResumeDto> GetDoctorResume(Guid id)
        {
            var response = await _doctorService.GetDoctorResumeAsync(id);
            return response;

        }

        [HttpGet("about/{id}")]
        public async Task<GetAboutDoctorDto> GetDoctorAbout(Guid id)
        {
            var response = await _doctorService.GetDoctorAboutAsync(id);
            return response;

        }
        [HttpGet("services/{id}")]
        public async Task<List<GetServiceDto>> GetDoctorService(Guid id)
        {
            var response = await _serviceService.GetServiceAsync(id);
            return response;

        }
        [HttpGet("own-availability")]
        public async Task<List<GetDoctorAvailabilityDto>> GetDoctorAvailability()
        {
            var response = await _doctorAvailabilityService.GetDoctorOwnAvailabilityAsync();
            return response;

        }
    }
}
