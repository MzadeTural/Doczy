using Doczy.Business.DTOs.PaymentDtos;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayriffController : ControllerBase
    {
        private IPayriffService _payriffService;

        public PayriffController(IPayriffService payriffService)
        {
            _payriffService = payriffService;
        }
        [HttpPost("pay")]
        public async Task Pay()
        {
            await _payriffService.Pay(1.1, "test");
        }
        [HttpPost("callback")]
        public IActionResult Callback(CallbackData callbackData)
        {
            // hansi user aciqdisa onu aliriq
            // sonuncunu aliriq
            //rrn payment succes edirsen nagarsan ele
            return Ok("sagol");
            //Newtonsoft.Json.JsonConvert.SerializeObject(callbackData);
        }
        [HttpPost("callbackDelete")]
        public IActionResult CallbackDelete()
        {
            // hansi user aciqdisa onu aliriq
            // sonuncunu aliriq
            // silirik
            return Ok();
        }
      

    }
}
