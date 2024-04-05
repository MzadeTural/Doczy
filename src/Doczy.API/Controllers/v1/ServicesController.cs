using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.ServiceDtos;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly IServiceService _serviceService;

        public ServicesController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }
        [HttpPost("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        public async Task<IActionResult> Create(CreateServiceDto createServiceDto)
        {

            var response = await _serviceService.CreateServiceAsync(createServiceDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));

        }

        [HttpGet("by-type/{doctorId}/{typeId}")]
        public async Task<List<GetServiceByTypeDto>> GetDoctorServiceByType([FromRoute] Guid doctorId , [FromRoute] Guid typeId)
        {
            var response = await _serviceService.GetServiceByTypeAsync(doctorId,typeId);
            return response;

        }
        [HttpGet("{doctorId}")]
        public async Task<List<GetServiceDto>> GetDoctorService([FromRoute] Guid doctorId)
        {
            var response = await _serviceService.GetServiceAsync(doctorId);
            return response;

        }

    }
}
