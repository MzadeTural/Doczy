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
            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(callbackData));
        }
      

    }
}
