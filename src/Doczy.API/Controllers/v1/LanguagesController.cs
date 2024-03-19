using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.HospitalDtos;
using Doczy.Business.DTOs.Language;
using Doczy.Business.Services.Implementations;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }
        [HttpPost("")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] CreateLanguageDto createLanguageDto)
        {
            var response = await _languageService.CreateLanguageAsync(createLanguageDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));

        }
        [HttpGet("")]
        public async Task<List<GetLanguageDto>> Get()
        {
            var languages = await _languageService.GetLanguageAsync();
            return languages;
        }

    }
}
