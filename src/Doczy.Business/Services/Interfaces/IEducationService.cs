using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.EducationDtos;

namespace Doczy.Business.Services.Interfaces
{
	public interface IEducationService
	{
        Task<EducationDto> GetEducation(Guid DoctorId);
        Task<ResponseDto> CreateEducationAsync(CreateEducationDto model);
    }
}

