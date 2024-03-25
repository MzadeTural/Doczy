using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.GenderDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IGenderService
    {
        Task<ResponseDto> CreateGender(CreateGenderDto model);
        Task<List<GetGenderDto>> GetGenders();
    }
}
