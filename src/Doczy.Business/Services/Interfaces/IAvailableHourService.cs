using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorAvailabilityDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IAvailableHourService
    {
        Task<ResponseDto> CreateAvailableHourAsync(CreateDoctorAvailabilityDto model);
    }
}
