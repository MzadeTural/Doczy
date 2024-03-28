using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.Experiance;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperiancesController : ControllerBase
    {
        private readonly IExperianceService _experianceService;

        public ExperiancesController(IExperianceService experianceService)
        {
            _experianceService = experianceService;
        }
        [HttpPost("")]
        public async Task<IActionResult> Create( CreateExperianceDto createExperianceDto)
        {
            var response = await _experianceService.CreateExperianceAsync(createExperianceDto);
            return StatusCode((int)response.StatusCode, response.Message);
        }
        [HttpPut("{experianceId}")]
        public async Task<IActionResult> Update([FromRoute] Guid experianceId, UpdateExperianceDto updateExperianceDto)
        {
            var response = await _experianceService.UpdateExperianceAsync(experianceId,updateExperianceDto);
            return StatusCode((int)response.StatusCode, response.Message);
        }
        [HttpGet("")]
        public async Task<List<GetExperianceDto>> Get()
        {
            var experiances = await _experianceService.GetExperiancesAsync();
            return experiances;
        }
    }
}
