using Doczy.Business.DTOs.AppointmentDto;
using Doczy.Business.DTOs.Common;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentsService;

        public AppointmentsController(IAppointmentService appointmentsService)
        {
            _appointmentsService = appointmentsService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment([FromForm] CreateAppointmentDto appointmentRequest)
        {
            var response = await _appointmentsService.CreateAppointmentAsync(appointmentRequest);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDto(response.StatusCode, response.Message));
        }

    }
}
