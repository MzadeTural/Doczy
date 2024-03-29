using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.ServiceTypeDtos;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypesController : ControllerBase
    {
        private readonly IServiceTypeService _serviceTypeService;

        public ServiceTypesController(IServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }

        [HttpPost]
        [Route("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] CreateServiceTypeDto createDto)
        {

            var response = await _serviceTypeService.CreateServiceTypeAsync(createDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));

        }
        [HttpPatch]
        [Route("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromForm] UpdateServiceTypeDto updateDto)
        {

            var response = await _serviceTypeService.UpdateServiceTypeAsync(id, updateDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));

        }
        [HttpGet("")]
        public async Task<List<GetServiceTypeDto>> Get()
        {
            var types = await _serviceTypeService.GetServiceTypeAsync();
            return types;
        }
    }
}
