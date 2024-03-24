using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorCategoryDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IDoctorCategoryService
    {
        Task<ResponseDto> CreateCategoryAsync(CreateDoctorCategoryDto model);
    }
}
