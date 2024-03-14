using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.ServiceDtos;
using Doczy.Business.DTOs.WorkPlace;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkPlacesController : ControllerBase
    {
        private readonly IHospitalService _workPlaceService;

        public WorkPlacesController(IHospitalService workPlaceService)
        {
            _workPlaceService = workPlaceService;
        }

        [HttpPost]
        [Route("create")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        public async Task<IActionResult> Create([FromForm] CreateHospitalDto createWPDto)
        {

            var response = await _workPlaceService.CreateWorkPlaceAsync(createWPDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));

        }
    }
}
