using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.ServiceDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IServiceService
    {
        Task<ResponseDto> CreateServiceAsync(CreateServiceDto model);
        Task<List<GetServiceDto>> GetServiceAsync(Guid doctorId);
        Task<List<GetServiceByTypeDto>> GetServiceByTypeAsync(Guid doctorId,Guid typeId);
    }
}
