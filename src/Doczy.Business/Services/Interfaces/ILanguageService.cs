using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.Language;

namespace Doczy.Business.Services.Interfaces
{
    public interface ILanguageService
    {
        Task<ResponseDto> CreateLanguageAsync(CreateLanguageDto model);
        Task<List<GetLanguageDto>> GetLanguageAsync();
    }
}
