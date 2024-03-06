using Doczy.Business.DTOs.Common;
using System.Net;
using Doczy.Business.DTOs.UnivercityDtos;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnivercitiesController : ControllerBase
    {
        private IUnivercityService _univercityService;

        public UnivercitiesController(IUnivercityService univercityService)
        {
            _univercityService = univercityService;
        }
        [Route("/create-univercity")]
        public async Task<IActionResult> CreateUnivercity([FromForm] CreateUnivercityDto createDoctorDto)
        {
            var response = await _univercityService.CreateUnivercityAsync(createDoctorDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [Route("/update-univercity")]
        public async Task<IActionResult> UpdateUnivercity(Guid id,[FromForm] UpdateUnivercityDto updateDoctorDto)
        {
            var response = await _univercityService.UpdateUnivercityAsync(id, updateDoctorDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [Route("/delete-univercity")]
        public async Task<IActionResult> DeleteUnivercity(Guid id)
        {
            var response = await _univercityService.DeleteUnivercityAsync(id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
    }
}
