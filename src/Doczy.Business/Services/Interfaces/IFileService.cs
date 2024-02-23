using Microsoft.AspNetCore.Http;

namespace Doczy.Business.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> CreateFileAsync(IFormFile file, string path);

        void DeteleFile(string path);
    }
}
