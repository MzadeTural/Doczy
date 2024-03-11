using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.FieldOfStudyDtos;
using Doczy.Business.Enums;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class FielfOfStudyController : ControllerBase
    {
        private IFieldOfStudyService _fieldOfStudyService;

        public FielfOfStudyController(IFieldOfStudyService fieldOfStudyService)
        {
            _fieldOfStudyService = fieldOfStudyService;
        }
        [HttpGet("get-fieldOfStudy/{id}")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetFieldOfStudy(Guid id)
        {
            var response = await _fieldOfStudyService.GetFieldOfStudyAsync(id);
            return Ok(response);
        }
        [HttpGet("get-fieldOfStudy")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetFieldOfStudy()
        {
            var response = await _fieldOfStudyService.GetAllFieldOfStudiesAsync();
            return Ok(response);
        }
        [HttpPost("create-fieldOfStudy")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateFieldOfStudy([FromForm] CreateFieldOfStudyDto createDoctorDto)
        {
            var response = await _fieldOfStudyService.CreateFieldOfStudyAsync(createDoctorDto);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpPatch("update-fieldOfStudy/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> UpdateFieldOfStudy(Guid id, [FromForm] UpdateFieldOfStudyDto updateDoctorDto)
        {
            var response = await _fieldOfStudyService.UpdateFieldOfStudy(id, updateDoctorDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpPut("delete-fieldOfStudy/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteFieldOfStudy(Guid id)
        {
            var response = await _fieldOfStudyService.DeleteFieldOfStudy(id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
    }
}
