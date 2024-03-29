using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.ServiceTypeDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IServiceTypeService
    {
        Task<ResponseDto> CreateServiceTypeAsync(CreateServiceTypeDto model);
        Task<List<GetServiceTypeDto>> GetServiceTypeAsync();
        Task<ResponseDto> UpdateServiceTypeAsync(Guid id, UpdateServiceTypeDto model);
    }
}
