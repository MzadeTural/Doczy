namespace Doczy.Business.Services.Interfaces
{
    public interface IVideoMeetingService
    {
        Task<string> CreateAsync(string patientName, string doctorName);
        Task<string> CreateZoomAsync(string meetName, int duration,string date,string time);
        Task<string> GetMeetingSpaceDataAsync();
    }
}
