using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.EducationDtos;
using Doczy.Business.Enums;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationsController : ControllerBase
    {
        private IEducationService _educationService;

        public EducationsController(IEducationService educationService)
        {
            _educationService = educationService;
        }
        [HttpGet("get-education/{doctorId}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetEducation(Guid doctorId)
        {
            var response = await _educationService.GetAllEducationsAsync(doctorId);
            return Ok(response);
        }
        [HttpPost("create-education")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateEducation([FromForm] CreateEducationDto createDoctorDto)
        {
            var response = await _educationService.CreateEducationAsync(createDoctorDto);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDto(response.StatusCode, response.Message));
        }
    }
}
