using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorAvailabilityDtos;
using Doczy.Business.DTOs.EducationDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IDoctorAvailabilityService
    {
        Task<ResponseDto> CreateDoctorAvailabilityAsync(CreateDoctorAvailabilityDto model);
        Task<ResponseDto> DeleteDoctorAvailability(Guid id);
    }
}
