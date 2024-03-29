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
        [HttpGet("doctor-eductions/{doctorId}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetDoctorEducation(Guid doctorId)
        {
            var response = await _educationService.GetAllEducationsAsync(doctorId);
            return Ok(response);
        }
        [HttpGet("{id}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetEducation(Guid id)
        {
            var response = await _educationService.GetEducationAsync(id);
            return Ok(response);
        }
        [HttpPost("")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        public async Task<IActionResult> CreateEducation(CreateEducationDto createDoctorDto)
        {
            var response = await _educationService.CreateEducationAsync(createDoctorDto);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> UpdateEducation(Guid id, UpdateEducationDto updateDoctorDto)
        {
            var response = await _educationService.UpdateEducation(id,updateDoctorDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteEducation(Guid id)
        {
            var response = await _educationService.DeleteEducation(id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
    }
}
