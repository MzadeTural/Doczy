using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.FavoriteDoctorDtos;
using Doczy.Business.DTOs.Language;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Business.Services.Implementations;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly IFavoriteDoctorService _favoriteDoctorService;
        public DoctorsController(IUserService userService, IDoctorService doctorService, IExperianceService experianceService, IDoctorAvailabilityService doctorAvailabilityService, IServiceService serviceService, IFavoriteDoctorService favoriteDoctorService)
        {

            _userService = userService;
            _doctorService = doctorService;
            _experianceService = experianceService;
            _doctorAvailabilityService = doctorAvailabilityService;
            _serviceService = serviceService;
            _favoriteDoctorService = favoriteDoctorService;
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
        [HttpGet("by-categroryId/{categoryId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDoctorsBycategory(Guid categoryId)
        {
            return Ok(await _doctorService.GetDoctorsByCategoryId(categoryId));
        }

        [HttpGet("will-verified")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetWillVerifiedDoctors()
        {
            return Ok(await _doctorService.GetWillVerifiedDoctors());
        }

        [HttpGet("filter")]
        public async Task<ActionResult<IEnumerable<GetDoctorsDto>>> FilterDoctors([FromQuery] GetDoctorFilterDto model)
        {
            var response = await _doctorService.GetFilterDoctors(model);
            return Ok(response);

        }


        [HttpPatch("update-phone")]
        public async Task<IActionResult> UpdatePhoneNumber( UserPhoneUpdateDto model)
        {
            var response = await _doctorService.UpdatePhoneNumberAsync(model);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpPost("add-language/{languageId}")]
        public async Task<IActionResult> AddLanguage([FromRoute] Guid languageId)
        {
            var response = await _doctorService.AddLanguageAsync(languageId);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpPatch("add-category/{categoryId}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] Guid categoryId)
        {
            var response = await _doctorService.UpdateCategoryAsync(categoryId);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }


        [HttpGet("languages")]
        public async Task<List<GetLanguageDto>> GetDoctorLanguages()
        {
            var languages = await _doctorService.GetLanguageAsync();
            return languages;
        }

        [HttpGet("resume/{doctorId}")]
        public async Task<GetDoctorResumeDto> GetDoctorResume([FromRoute] Guid doctorId)
        {
            var response = await _doctorService.GetDoctorResumeAsync(doctorId);
            return response;

        }

        [HttpGet("about/{doctorId}")]
        public async Task<GetAboutDoctorDto> GetDoctorAbout([FromRoute] Guid doctorId)
        {
            var response = await _doctorService.GetDoctorAboutAsync(doctorId);
            return response;

        }

        [HttpGet("detail/{doctorId}")]
        public async Task<GetDoctorDetailDto> GetDoctorDetail([FromRoute] Guid doctorId)
        {
            var response = await _doctorService.GetDoctorDetailAsync(doctorId);
            return response;

        }

        [HttpPost("add-to-favourite/{doctorId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        public async Task<IActionResult> AddFavouriteDoctor([FromRoute]Guid doctorId)
        {
            var response = await _favoriteDoctorService.CreateFavoriteDoctorAsync(doctorId);

            return StatusCode((int)response.StatusCode, response.Message);
        }

        [HttpDelete("remove-to-favourite/{doctorId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        public async Task<IActionResult> RemoveFavouriteDoctor([FromRoute]Guid doctorId)
        {
            var response = await _favoriteDoctorService.RemoveFavoriteDoctorAsync(doctorId);

            return StatusCode((int)response.StatusCode, response.Message);
        }
    }
}
