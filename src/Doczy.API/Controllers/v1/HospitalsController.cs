using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.Experiance;
using Doczy.Business.DTOs.HospitalDtos;
using Doczy.Business.DTOs.ServiceDtos;
using Doczy.Business.DTOs.WorkPlace;
using Doczy.Business.Services.Implementations;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalsController : ControllerBase
    {
        private readonly IHospitalService _hospitalService;

        public HospitalsController(IHospitalService workPlaceService)
        {
            _hospitalService = workPlaceService;
        }

        [HttpPost]
        [Route("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] CreateHospitalDto createHDto)
        {

            var response = await _hospitalService.CreateHospitalAsync(createHDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));

        }
        [HttpGet("")]
        public async Task<List<GetHospitalDto>> Get()
        {
            var hospitals = await _hospitalService.GetHospitalAsync();
            return hospitals;
        }
        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetHospitalById(Guid Id)
        {
            var hospitals = await _hospitalService.GetHospitalByIdAsync(Id);
            return Ok(hospitals);
        }
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteHospital(Guid Id)
        {
            var response = await _hospitalService.DeleteHospitalAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpPatch("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> Update([FromRoute]Guid Id,[FromForm]UpdateHospitalDto hospitalDto)
        {
            var response = await _hospitalService.UpdateHospitalAsync(Id,hospitalDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
    }
}
