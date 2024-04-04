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
        private readonly IVideoMeetingService _videoMeetingService;

        public AppointmentsController(IAppointmentService appointmentsService, IVideoMeetingService videoMeetingService)
        {
            _appointmentService = appointmentsService;
            _videoMeetingService = videoMeetingService;
        }

        [HttpPost("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Patient")]
        public async Task<IActionResult> Createt( CreateAppointmentDto appointmentRequest)
        {
            var response = await _appointmentService.CreateAppointmentAsync(appointmentRequest);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpGet("test")]
        public async Task<IActionResult> testw()
        {
            return Ok(DateTime.Now.AddMinutes(-15));
        }
        [HttpGet("meet")]
        //[Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        public async Task<IActionResult> GetMeet()
        {
            return Ok(await _videoMeetingService.GetMeetingSpaceDataAsync());
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
