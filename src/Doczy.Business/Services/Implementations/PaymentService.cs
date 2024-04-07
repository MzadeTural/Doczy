using Doczy.Business.DTOs.AppointmentDto;
using Doczy.Business.DTOs.PaymentDtos;
using Doczy.Business.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace Doczy.Business.Services.Implementations
{
    public class PaymentService : IPaymentService
    {
        private IConfiguration _configre;


        public PaymentService(IConfiguration configre)
        {

            _configre = configre;

        }

        public decimal CalculatePaymentAmount(CreateAppointmentDto model)
        {
            // Sample calculation based on service or any other relevant factors
            return 100.00m; // Example amount
        }

        public async Task<string> InitiatePaymentAsync(double sumAmount, string desc)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
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
                    // Serialize payment request to JSON
                    var jsonRequestBody = JsonConvert.SerializeObject(requestBody);

                    // Create HTTP request message
                    var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://api.payriff.com/api/v2/createOrder");
                    httpRequest.Content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                    // Send HTTP request to payment system
                    var response = await httpClient.SendAsync(httpRequest);

                    // Ensure successful response
                    response.EnsureSuccessStatusCode();

                    // Parse payment URL from response body
                    var responseContent = await response.Content.ReadAsStringAsync();
                    dynamic responseObject = JsonConvert.DeserializeObject(responseContent);
                    string paymentUrl = responseObject.paymentUrl;

                    return paymentUrl;
                }
            }
            catch (Exception ex)
            {
                // Handle exception, log error, etc.
                throw new Exception("Failed to initiate payment", ex);
            }
        }


        public async Task<HttpResponseMessage> MakePaymentRequestAsync(string endpoint, decimal amount, string description)
        {
            using (var httpClient = new HttpClient())
            {
                var requestBody = new
                {
                    body = new
                    {
                        amount = amount,
                        approveURL = _configre["Payriff:approveURL"].ToString(),
                        cancelURL = _configre["Payriff:cancelURL"].ToString(),
                        declineURL = _configre["Payriff:declineURL"].ToString(),
                        cardUuid = "string",
                        currencyType = "AZN",
                        description = description,
                        directPay = true,
                        installmentPeriod = 0,
                        installmentProductType = "BIRKART",
                        language = "AZ",
                        senderCardUID = "string"
                    },
                    merchant = _configre["Payriff:merchant"].ToString()
                };
                var requestUrl = $"https://api.payriff.com/api/v2/{endpoint}";

                // Serialize request body to JSON
                var jsonRequestBody = JsonConvert.SerializeObject(requestBody);

                // Create HTTP request message
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl);
                httpRequest.Content = new StringContent(jsonRequestBody, Encoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Add("Authorization", _configre["Payriff:secretKey"].ToString());
                // Send HTTP request
                var response = await httpClient.SendAsync(httpRequest);

                return response;
            }
        }

        public PayriffResponseDto ParsePaymentDataFromResponse(HttpResponseMessage response)
        {

            if (response.IsSuccessStatusCode)
            {
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var responseObject = JsonConvert.DeserializeObject<PayriffResponseDto>(responseContent);
                var url = responseObject.Payload.PaymentUrl;
                return responseObject; // Assuming the payment URL is returned as "paymentUrl" in the response
            }
            else
            {
                // Handle error response
                throw new Exception("Failed to parse payment URL from response");
            }
        }

        public async Task RedirectUserToPayment(string paymentUrl)
        {
            // Assuming this method is called within a controller action where you have access to the HttpContext

            // _contextAccessor.Response.Redirect(paymentUrl);
            Process.Start(new ProcessStartInfo
            {
                FileName = paymentUrl,
                UseShellExecute = true
            });

        }
        public async Task HandlePaymentCallbackAsync(CallbackData paymentCallback)
        {
            try
            {
                // Deserialize the payment callback JSON to an object
                // Here, PaymentCallbackDto is your DTO representing the structure of the callback
                // Make sure to replace PaymentCallbackDto with your actual DTO class
                var jsonData = JsonConvert.SerializeObject(paymentCallback);

                // Deserialize JSON data to PaymentCallbackDto object
                var callbackData = JsonConvert.DeserializeObject<CallbackData>(jsonData);
                /// var callbackData = JsonConvert.SerializeObject(paymentCallback));
                // Perform any necessary validation or processing based on the callback data
                // For example, you might check the payment status, update the database, etc.

                // Example: Update database with payment status
                await UpdatePaymentStatus(callbackData.Payload.OrderId, callbackData.Payload.OrderStatus);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during callback processing
                // You might want to log the exception or perform other error-handling actions
                Console.WriteLine($"Error handling payment callback: {ex.Message}");
                throw; // Rethrow the exception if necessary
            }
        }

        private async Task UpdatePaymentStatus(int orderId, string status)
        {
            // Implement logic to update the payment status in the database
            // This method will depend on your database access mechanism (e.g., Entity Framework, Dapper)
            // Example:
            // await _paymentRepository.UpdateStatusAsync(paymentId, status);
        }
    }
}
