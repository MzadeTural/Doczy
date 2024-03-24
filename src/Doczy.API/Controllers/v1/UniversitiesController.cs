using Doczy.Business.DTOs.Common;
using System.Net;
using Doczy.Business.DTOs.UnivercityDtos;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : ControllerBase
    {
        private IUnivercityService _univercityService;

        public UniversitiesController(IUnivercityService univercityService)
        {
            _univercityService = univercityService;
        }
        [HttpGet("{id}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetUnivercity(Guid id)
        {
            var response = await _univercityService.GetUnivercityAsync(id);
            return Ok(response);
        }
        [HttpGet("")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetUnivercity()
        {
            var response = await _univercityService.GetUnivercitiesAsync();
            return Ok(response);
        }
        [HttpPost("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateUnivercity( CreateUnivercityDto createDoctorDto)
        {
            var response = await _univercityService.CreateUnivercityAsync(createDoctorDto);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> UpdateUnivercity(Guid id, UpdateUnivercityDto updateDoctorDto)
        {
            var response = await _univercityService.UpdateUnivercity(id, updateDoctorDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteUnivercity(Guid id)
        {
            var response = await _univercityService.DeleteUnivercity(id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
    }
}
