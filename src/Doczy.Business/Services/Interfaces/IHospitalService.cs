using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.HospitalDtos;
using Doczy.Business.DTOs.WorkPlace;

namespace Doczy.Business.Services.Interfaces
{
    public interface IHospitalService
    {
        Task<ResponseDto> CreateHospitalAsync(CreateHospitalDto model);
        Task<ResponseDto> UpdateHospitalAsync(Guid hospitalId,UpdateHospitalDto model);
        Task<ResponseDto> DeleteHospitalAsync(Guid hospitalId);
        Task<List<GetHospitalDto>> GetHospitalAsync();
        Task<GetHospitalDto> GetHospitalByIdAsync(Guid hospitalId);
    }
}
