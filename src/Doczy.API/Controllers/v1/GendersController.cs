using Doczy.Business.DTOs.AppointmentDto;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.GenderDtos;
using Doczy.Business.Services.Implementations;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {
        private readonly IGenderService _genderService;

        public GendersController(IGenderService genderService)
        {
            _genderService = genderService;
        }

        [HttpPost("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> Create( CreateGenderDto createDto)
        {
            var response = await _genderService.CreateGender(createDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _genderService.GetGenders());
        }
    }
}
