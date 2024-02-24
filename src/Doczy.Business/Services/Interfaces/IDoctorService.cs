using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<List<GetDoctorAppointmentsDto>> GetDoctorAppointments(Guid userId);
        Task<ResponseDto> UpdatePhoneNumberAsync(Guid id, UserPhoneUpdateDto model);
        Task<ResponseDto> UpdateWorkPlaceAsync(Guid id, Guid worpPlaceId);
    }
}
