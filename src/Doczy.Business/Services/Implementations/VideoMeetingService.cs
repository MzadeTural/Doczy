using Doczy.Business.Services.Interfaces;
using System.Net;
using Vonage;
using Vonage.Meetings.CreateRoom;
using Vonage.Request;

namespace Doczy.Business.Services.Implementations
{
    public class VideoMeetingService : IVideoMeetingService
    {
        public async Task<string> CreateAsync(string patientName, string doctorName)
        {
            var credentials = Credentials.FromApiKeyAndSecret(
                    "1b69763b",
                    "CtngRpBpDK4F4HmS"
                    );

            var applicationId = Environment.GetEnvironmentVariable("VONAGE_APP_ID") ?? "VONAGE_APP_ID";
            var privateKeyPath = Environment.GetEnvironmentVariable("VONAGE_PRIVATE_KEY_PATH") ?? "VONAGE_PRIVATE_KEY_PATH";
            //  var credentials = Credentials.FromAppIdAndPrivateKeyPath(applicationId, privateKeyPath);

            var displayName = Environment.GetEnvironmentVariable("Doctor_And_Patient") ?? $"{patientName}_And_{doctorName}";
            var client = new VonageClient(credentials);
            var request = CreateRoomRequest.Build()
                
                .WithDisplayName(displayName)
                .Create();
            var response = await client.MeetingsClient.CreateRoomAsync(request);
            var message = response.Match(
                success => $"{success.Links.GuestUrl.Href}",
                failure => $"Room creation failed: {failure.GetFailureMessage()}");
            return message;
        }
    }
}
