using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.EducationDtos;

namespace Doczy.Business.Services.Interfaces
{
	public interface IEducationService
	{
        Task<List<EducationDto>> GetAllEducationsAsync(Guid DoctorId);
        Task<ResponseDto> CreateEducationAsync(CreateEducationDto model);
    }
}

