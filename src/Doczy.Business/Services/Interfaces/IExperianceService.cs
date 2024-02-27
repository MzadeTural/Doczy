using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.Experiance;

namespace Doczy.Business.Services.Interfaces
{
    public interface IExperianceService
    {
        Task<ResponseDto> CreateExperianceAsync(CreateExperianceDto model);
    }
}
