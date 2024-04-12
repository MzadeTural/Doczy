using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.DTOs.Language;

namespace Doczy.Business.Services.Interfaces
{
    public interface IDoctorService
    {

        Task<GetAboutDoctorDto> GetDoctorAboutAsync(Guid doctorId);
        Task<GetDoctorDetailDto> GetDoctorDetailAsync(Guid doctorId);
        Task<GetDoctorProfileDto> GetDoctorProfileAsync();
        Task<GetDashboardReports> GetDashboardReportsAsync();
        
        Task<List<GetDoctorsDto>> GetFilterDoctors(GetDoctorFilterDto model);
        Task<List<GetDoctorsDto>> GetDoctors();
        Task<List<GetDoctorsDto>> GetDoctorsPaginate(int pageIndex, int pageSize);
        Task<List<GetDoctorsDto>> GetDoctorsByCategoryId(Guid categoryId);
        Task<List<GetWillVerifiedDoctorDto>> GetWillVerifiedDoctors();
        Task<GetDoctorResumeDto> GetDoctorResumeAsync(Guid doctorId);
        Task<ResponseDto> UpdatePhoneNumberAsync(UserPhoneUpdateDto model);

        Task<ResponseDto> AddLanguageAsync(Guid languageId);
        Task<ResponseDto> UpdateCategoryAsync(Guid categoryId);
        Task<List<GetLanguageDto>> GetLanguageAsync();
    }
}
