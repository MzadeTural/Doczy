using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.PaymentDtos;
using Doczy.Business.Exceptions.PaymentExceptions;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        public async Task<IActionResult> CreatePayment( decimal amount, string desc)
        {
            try
            {
                var paymentResponse = await _paymentService.MakePaymentRequestAsync("createOrder", amount,  desc);
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
        public async Task<IActionResult> PaymentCallback()
        {
            var formValues = await Request.ReadFormAsync();
            return Ok(formValues);
            //using (StreamReader reader = new StreamReader(Request.Body))
            //{
            //    string body = await reader.ReadToEndAsync();
            //    //JObject jsonObject = JObject.Parse(body);
            //    CallbackData response = JsonConvert.DeserializeObject<CallbackData>(body);

            //    // JSON içeriğini konsola yazdırma
            //    return Ok(data.ToString());
            //}
            //var response = await _appointmentService.UpdateAppointmentPaymentStatusAsync(paymentCallback);
            //return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
    }
}
