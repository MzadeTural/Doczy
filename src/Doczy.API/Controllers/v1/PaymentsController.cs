using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.PaymentDtos;
using Doczy.Business.Exceptions.PaymentExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IAppointmentService _appointmentService;

        public PaymentsController(IPaymentService paymentService, IAppointmentService appointmentService)
        {
            _paymentService = paymentService;
            _appointmentService = appointmentService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePayment(CreatePayment createPayment)
        {
            try
            {
                var paymentResponse = await _paymentService.MakePaymentRequestAsync("createOrder", createPayment);
                var parsePaymentResponse = _paymentService.ParsePaymentDataFromResponse(paymentResponse);
                var paymentUrl = parsePaymentResponse.Payload.PaymentUrl;
                return Ok(new { PaymentUrl = paymentUrl });
            }
            catch (PaymentFailedException ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { Message = ex.Message });
            }
        }

        [HttpPost("callback")]
        public async Task<IActionResult> PaymentCallback(CallbackData paymentCallback)
        {
            //return Ok(paymentCallback);
            var response = await _appointmentService.UpdateAppointmentPaymentStatusAsync(paymentCallback);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }

        [HttpGet("callback")]
        public IActionResult PaymentCallback()
        {
            //return Ok(paymentCallback);

            return Redirect("http://localhost:3000/Doctors/Confirm");
        }

    }
}
