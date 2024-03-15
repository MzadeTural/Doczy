using Doczy.Business.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text;
using Doczy.Core.Entities;
using System.Diagnostics;

namespace Doczy.Business.Services.Implementations
{
    public class PayriffService : IPayriffService
    {
        private IConfiguration _configre;

        public PayriffService(IConfiguration configre)
        {
            _configre = configre;
        }
        public async Task Payment(string url)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }
        public async Task Pay(double sumAmount,string desc)
        {
            var api = _configre["Payriff:apiurl"].ToString() + "createOrder";
            var requestBody = new
            {
                body = new
                {
                    amount = sumAmount,
                    approveURL = _configre["Payriff:approveURL"].ToString(),
                    cancelURL = _configre["Payriff:cancelURL"].ToString(),
                    declineURL = _configre["Payriff:declineURL"].ToString(),
                    cardUuid = "string",
                    currencyType = "AZN",
                    description = desc,
                    directPay = true,
                    installmentPeriod = 0,
                    installmentProductType = "BIRKART",
                    language = "AZ",
                    senderCardUID = "string"
                },
                merchant = _configre["Payriff:merchant"].ToString()
            };

            string jsonBody = System.Text.Json.JsonSerializer.Serialize(requestBody);
            var response = await HttpRequest(api,jsonBody);

            var responseObject = System.Text.Json.JsonSerializer.Deserialize<Payriff>(response);
            Payriff.Payload payload = responseObject.payload;
           await Payment(payload.PaymentUrl);

        }
        private async Task<string> HttpRequest(string apiUrl, string jsonBody)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", _configre["Payriff:secretKey"].ToString());

                HttpResponseMessage response = await client.PostAsync(apiUrl, new StringContent(jsonBody, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                else
                {
                    return response.StatusCode.ToString();
                }
            }
        }
    }
}
