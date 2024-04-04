namespace Doczy.Business.Services.Interfaces
{
    public interface IVideoMeetingService
    {
        Task<string> CreateAsync(string patientName, string doctorName);
        Task<string> GetMeetingSpaceDataAsync();
    }
}
