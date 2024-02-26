using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.Language;
using Doczy.Business.DTOs.ServiceDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface ILanguageService
    {
        Task<ResponseDto> CreateLanguageAsync(CreateLanguageDto model);
    }
}
