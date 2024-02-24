using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.ServiceDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IServiceService
    {
        Task<ResponseDto> CreateServiceAsync(CreateServiceDto model);
    }
}
