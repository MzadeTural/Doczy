using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.EducationDtos;

namespace Doczy.Business.Services.Interfaces
{
	public interface IEducationService
	{
        Task<List<EducationDto>> GetAllEducationsAsync(Guid DoctorId);
        Task<EducationDto> GetEducationAsync(Guid id);
        Task<ResponseDto> CreateEducationAsync(CreateEducationDto model);
        Task<ResponseDto> UpdateEducation(Guid id, UpdateEducationDto model);
        Task<ResponseDto> DeleteEducation(Guid id);
    }
}

