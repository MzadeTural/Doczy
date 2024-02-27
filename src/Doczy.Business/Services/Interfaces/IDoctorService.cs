using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.Language;

namespace Doczy.Business.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<List<GetDoctorAppointmentsDto>> GetDoctorAppointments(Guid userId);
        Task<ResponseDto> UpdatePhoneNumberAsync(Guid id, UserPhoneUpdateDto model);
        Task<ResponseDto> UpdateWorkPlaceAsync(Guid id, Guid worpPlaceId);
        Task<ResponseDto> AddLanguageAsync(Guid languageId);
         Task<List<GetLanguageDto>> GetLanguageAsync(Guid userId);
    }
}
