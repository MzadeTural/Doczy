using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.Experiance;
using Doczy.Business.DTOs.HospitalDtos;
using Doczy.Business.DTOs.ServiceDtos;
using Doczy.Business.DTOs.WorkPlace;
using Doczy.Business.Services.Implementations;
using Doczy.Business.Services.Interfaces;
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
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
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
    }
}
