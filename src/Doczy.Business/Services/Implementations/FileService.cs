using Doczy.Business.Exceptions.FileExceptions;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Doczy.Business.Services.Implementations
{
    public class FileService : IFileService
    {
        public async Task<string> CreateFileAsync(IFormFile file, string path)
        {
            if (!file.ContentType.Contains("image/"))
            {
                throw new FileTypeException("file type not supported");
            }
            if (file.Length / 1024 > 600)
            {
                throw new FileSizeException("file too large, you can upload files up to 0.6 MB");
            }
            string FileName = $"{Guid.NewGuid()}-{file.FileName}";
            string ResultPath = Path.Combine(path, FileName);
            using (FileStream fileStream = new FileStream(ResultPath, FileMode.Create))
            {
              await file.CopyToAsync(fileStream);
            }

            return FileName;

        }

        public void DeteleFile(string path)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
        }
    }
}
