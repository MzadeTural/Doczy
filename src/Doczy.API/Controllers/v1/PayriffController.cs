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
        [HttpPost("pay")]
        public async Task<IActionResult> Pay()
        {
            string apiUrl = "https://api.payriff.com/api/v2/createOrder";

            // Authorization Header
            string secretKey = "119F3C882DFD485BA4A97DF092F5E542";

            // JSON verisini oluştur
            var requestBody = new
            {
                body = new
                {
                    amount = 1,
                    approveURL = "https://payriff.com/",
                    cancelURL = "string",
                    declineURL = "string",
                    cardUuid = "string",
                    currencyType = "AZN",
                    description = "Test",
                    directPay = true,
                    installmentPeriod = 0,
                    installmentProductType = "BIRKART",
                    language = "AZ",
                    senderCardUID = "string"
                },
                merchant = "ES1092709"

                //amount = 1,
                //orderId= "760144"

            };

            // JSON verisini stringe çevir
            string jsonBody = JsonSerializer.Serialize(requestBody);

            // HTTP isteği oluştur
            using (HttpClient client = new HttpClient())
            {
                // Başlık ekle
                client.DefaultRequestHeaders.Add("Authorization", secretKey);

                // JSON verisi içeren bir POST isteği gönder
                HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));

                // Yanıtı kontrol et
                if (response.IsSuccessStatusCode)
                {
                    // Yanıt içeriğini al
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return Ok(responseBody);
                }
                else
                {
                    return Ok(response.StatusCode);
                }
            }
        }
    }
}
