using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.HospitalDtos;
using Doczy.Business.DTOs.WorkPlace;

namespace Doczy.Business.Services.Interfaces
{
    public interface IHospitalService
    {
        Task<ResponseDto> CreateHospitalAsync(CreateHospitalDto model);
        Task<List<GetHospitalDto>> GetHospitalAsync();
    }
}
