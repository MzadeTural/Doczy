using Doczy.Business.DTOs.VideoDtos;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text.Json;
using Vonage;
using Vonage.Meetings.CreateRoom;
using Vonage.Request;

namespace Doczy.Business.Services.Implementations
{
    public class VideoMeetingService : IVideoMeetingService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        public VideoMeetingService(IHttpContextAccessor httpContextAccessor, HttpClient httpClient)
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClient;
        }

        public async Task<string> CreateAsync(string patientName, string doctorName)
        {
            
            //var credentials = Credentials.FromApiKeyAndSecret(
            //        "1b69763b",
            //        "CtngRpBpDK4F4HmS"
            //        );

            //var applicationId = Environment.GetEnvironmentVariable("VONAGE_APP_ID") ?? "VONAGE_APP_ID";
            //var privateKeyPath = Environment.GetEnvironmentVariable("VONAGE_PRIVATE_KEY_PATH") ?? "VONAGE_PRIVATE_KEY_PATH";
            ////  var credentials = Credentials.FromAppIdAndPrivateKeyPath(applicationId, privateKeyPath);

            //var displayName = Environment.GetEnvironmentVariable("Doctor_And_Patient") ?? $"{patientName}_And_{doctorName}";
            //var client = new VonageClient(credentials);
            //var request = CreateRoomRequest.Build()
                
            //    .WithDisplayName(displayName)
            //    .Create();
            //var response = await client.MeetingsClient.CreateRoomAsync(request);
            //var message = response.Match(
            //    success => $"{success.Links.GuestUrl.Href}",
            //    failure => $"Room creation failed: {failure.GetFailureMessage()}");
            return "message";


            
        }

        public async Task<string> CreateZoomAsync(string meetName, int duration, string date, string time)
        {
            using (HttpClient client = new HttpClient())
            {
                // Base URL needs to be defined.
                var baseUrl = "https://doczy.000webhostapp.com/";
                var url = $"{baseUrl}?name={Uri.EscapeDataString(meetName)}&duration={duration}&date={Uri.EscapeDataString(date)}&time={Uri.EscapeDataString(time)}";

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the JSON response into a C# object
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    ZoomApiResponseDto apiResponse = JsonSerializer.Deserialize<ZoomApiResponseDto>(jsonResponse);

                    // Access the join_url property
                    return apiResponse.message.response.join_url;
                }
                else
                {
                    return $"Error: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}";
                }
            }
        }

        public async Task<string> GetMeetingSpaceDataAsync()
        {
            string result="";
            using (HttpClient client = new HttpClient())
            {
                string url = "https://google-meet-nodejs.onrender.com/api/create-meeting-space";
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                     result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(result);
                }
            }
            return result;
        }
    }
}
