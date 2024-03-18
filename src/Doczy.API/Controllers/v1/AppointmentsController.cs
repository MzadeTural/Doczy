using Doczy.Business.DTOs.AppointmentDto;
using Doczy.Business.DTOs.Common;
using Doczy.Business.Exceptions.PaymentExceptions;
using Doczy.Business.Exceptions.ServiceExceptions;
using Doczy.Business.Services.Implementations;
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
        public async Task<IActionResult> CreateAppointment([FromForm] CreateAppointmentDto appointmentRequest)
        {
           
                var response= await _appointmentService.CreateAppointmentAsync(appointmentRequest);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }

        [HttpGet("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
        public async Task<IActionResult> GetAppointments()
        {
            return Ok(await _appointmentService.GetAppointmentAsync());
        }

    }
}
