using Doczy.Business.DTOs.CategorySliderDtos;
using Doczy.Business.DTOs.Common;

namespace Doczy.Business.Services.Interfaces
{
    public interface ICategorySliderService
    {
        Task<ResponseDto> CreateAsync(CreateCategorySliderDto model);
        Task<List<GetCategorySliderDto>> GetAsync();
        Task<GetCategorySliderDto> GetByIdAsync(Guid id);
        Task<ResponseDto> DeleteAsync(Guid id);
    }
}
