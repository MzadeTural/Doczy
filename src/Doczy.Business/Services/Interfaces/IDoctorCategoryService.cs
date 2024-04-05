using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorCategoryDtos;
using Doczy.Business.DTOs.Experiance;

namespace Doczy.Business.Services.Interfaces
{
    public interface IDoctorCategoryService
    {
        
        Task<ResponseDto> CreateCategoryAsync(CreateDoctorCategoryDto model);
        Task<List<GetDoctorCategoryDto>> GetCategoryAsync();
        Task<List<GetEntityIdDto>> GetCategoryIdAsync();
    }
}
