using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.Language;
using Doczy.Business.DTOs.RaitingDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<List<GetDoctorAppointmentsDto>> GetDoctorAppointments();
        Task<GetAboutDoctorDto> GetDoctorAboutAsync(Guid doctorId);
        Task<List<GetDoctorsDto>> GetFilterDoctors(GetDoctorFilterDto model);
        Task<List<GetDoctorsDto>> GetDoctors();
        Task<GetDoctorResumeDto> GetDoctorResumeAsync(Guid doctorId);
        Task<ResponseDto> UpdatePhoneNumberAsync( UserPhoneUpdateDto model);
        Task<ResponseDto> UpdateWorkPlaceAsync(Guid id, Guid worpPlaceId);
        Task<ResponseDto> AddLanguageAsync(Guid languageId);
        Task<ResponseDto> UpdateCategoryAsync(Guid categoryId);
        Task<List<GetLanguageDto>> GetLanguageAsync();
    }
}
