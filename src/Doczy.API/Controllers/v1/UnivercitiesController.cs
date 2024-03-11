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
    public class UnivercitiesController : ControllerBase
    {
        private IUnivercityService _univercityService;

        public UnivercitiesController(IUnivercityService univercityService)
        {
            _univercityService = univercityService;
        }
        [HttpGet("get-univercity/{id}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetUnivercity(Guid id)
        {
            var response = await _univercityService.GetUnivercityAsync(id);
            return Ok(response);
        }
        [HttpGet("get-univercity")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetUnivercity()
        {
            var response = await _univercityService.GetUnivercitiesAsync();
            return Ok(response);
        }
        [HttpPost("create-univercity")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateUnivercity([FromForm] CreateUnivercityDto createDoctorDto)
        {
            var response = await _univercityService.CreateUnivercityAsync(createDoctorDto);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpPatch("update-univercity/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> UpdateUnivercity(Guid id,[FromForm] UpdateUnivercityDto updateDoctorDto)
        {
            var response = await _univercityService.UpdateUnivercity(id, updateDoctorDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpPut("delete-univercity/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteUnivercity(Guid id)
        {
            var response = await _univercityService.DeleteUnivercity(id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
    }
}
