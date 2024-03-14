using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.ServiceTypeDtos;
using Doczy.Business.DTOs.WorkPlace;

namespace Doczy.Business.Services.Interfaces
{
    public interface IHospitalService
    {
        Task<ResponseDto> CreateWorkPlaceAsync(CreateHospitalDto model);
    }
}
