using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UnivercityDegreeDtos;
using Doczy.Business.Enums;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnivercityDegreeController : ControllerBase
    {
        private IUnivercityDegreeService _univercityDegreeService;

        public UnivercityDegreeController(IUnivercityDegreeService UnivercityDegreeService)
        {
            _univercityDegreeService = UnivercityDegreeService;
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetUnivercityDegree(Guid id)
        {
            var response = await _univercityDegreeService.GetUnivercityDegreeAsync(id);
            return Ok(response);
        }
        [HttpGet("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor,Admin,Patient")]
        public async Task<IActionResult> GetUnivercityDegree()
        {
            var response = await _univercityDegreeService.GetAllUnivercityDegreesAsync();
            return Ok(response);
        }
        [HttpPost("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateUnivercityDegree(CreateUnivercityDegreeDto createUnivercityDegreeDto)
        {
            var response = await _univercityDegreeService.CreateUnivercityDegreeAsync(createUnivercityDegreeDto);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> UpdateUnivercityDegree(Guid id, UpdateUnivercityDegreeDto updateUnivercityDegreeDto)
        {
            var response = await _univercityDegreeService.UpdateUnivercityDegree(id, updateUnivercityDegreeDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteUnivercityDegree(Guid id)
        {
            var response = await _univercityDegreeService.DeleteUnivercityDegree(id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
    }
}
