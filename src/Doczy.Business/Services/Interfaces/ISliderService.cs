using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.SliderDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface ISliderService
    {
        Task<ResponseDto> CreateAsync(CreateSliderDto model);
        Task<List<GetSliderDto>> GetAsync();
        Task<ResponseDto> Delete(Guid id);
    }
}
