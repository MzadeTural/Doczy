using Doczy.Business.DTOs.AppointmentDto;
using Doczy.Business.DTOs.Common;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentsService)
        {
            _appointmentService = appointmentsService;
        }

        [HttpPost("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        public async Task<IActionResult> Createt([FromForm] CreateAppointmentDto appointmentRequest)
        {
            var response = await _appointmentService.CreateAppointmentAsync(appointmentRequest);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }

        [HttpGet("doctor")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        public async Task<IActionResult> GetDoctorAppointments()
        {
            return Ok(await _appointmentService.GetDoctorAppointmentAsync());
        }
        [HttpGet("patient")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        public async Task<IActionResult> GetPatientAppointments()
        {
            return Ok(await _appointmentService.GetPatientAppointmentAsync());
        }

    }
}
