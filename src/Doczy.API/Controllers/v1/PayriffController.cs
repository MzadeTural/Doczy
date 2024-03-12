using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
            await _payriffService.Pay(1.1, "test");
            return Ok();
        }
    }
}
