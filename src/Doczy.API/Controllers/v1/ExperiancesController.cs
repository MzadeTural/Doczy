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

        [HttpGet("/experiances")]
        public async Task<List<GetExperianceDto>> GetDoctorAppointments()
        {
            var experiances = await _experianceService.GetExperiancesAsync();
            return experiances;
        }
    }
}
