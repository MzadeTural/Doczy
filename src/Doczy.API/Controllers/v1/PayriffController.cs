using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

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
        public async Task<IActionResult> Pay()
        {
            var res = await _payriffService.Pay(1, "test");

            //Response.Redirect(res.payload.paymentUrl);

            return Ok(res);

           
        }
    }
}
