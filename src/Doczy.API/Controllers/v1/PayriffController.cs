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
        public class CallbackData
        {
            public Payload payload { get; set; }
            public string code { get; set; }
            public string message { get; set; }
            public string route { get; set; }
        }

        public class Payload
        {
            public string version { get; set; }
            public int orderID { get; set; }
            public string sessionId { get; set; }
            public string transactionType { get; set; }
            public string RRN { get; set; }
            public string PAN { get; set; }
            public int purchaseAmount { get; set; }
            public string currency { get; set; }
            public string tranDateTime { get; set; }
            public string responseCode { get; set; }
            public string responseDescription { get; set; }
            public string brand { get; set; }
            public string orderStatus { get; set; }
            public string approvalCode { get; set; }
            public string orderDescription { get; set; }
            public string approvalCodeScr { get; set; }
            public int purchaseAmountScr { get; set; }
            public string currencyScr { get; set; }
            public string orderStatusScr { get; set; }
            public CardRegistration cardRegistration { get; set; }
            public string invoiceUuid { get; set; }
        }

        public class CardRegistration
        {
            public string MaskedPAN { get; set; }
            public string CardUID { get; set; }
            public string Brand { get; set; }
        }

    }
}
