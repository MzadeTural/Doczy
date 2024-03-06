using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.Experiance;
using Doczy.Business.DTOs.Language;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography;
using System.Text;

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
        [HttpPatch("{languageId}/add-language")]
        public async Task<IActionResult> AddLanguage(Guid languageId)
        {
            var response = await _doctorService.AddLanguageAsync(languageId);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [Authorize]
        [HttpPatch("{categoryId}/add-category")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId)
        {
            var response = await _doctorService.UpdateCategoryAsync(categoryId);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }

        [HttpPost("experiance")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
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

        [HttpPost("test")]
        public async Task<string> test(  )
        {
           
                string userId = "0a456e14-dc29-442e-60c5-08dc371121a0";
            string secretKey = "08a5d4e8-86c3-4780-9761-318f3349b3ae";

                string data = userId;
                byte[] keyBytes = Encoding.UTF8.GetBytes(secretKey);
                byte[] dataBytes = Encoding.UTF8.GetBytes(data);

                using (var hmac = new HMACSHA256(keyBytes))
                {
                    byte[] hashBytes = hmac.ComputeHash(dataBytes);
                    string token = Convert.ToBase64String(hashBytes);
                    return token;
                }                       
        }
        

        }
}
