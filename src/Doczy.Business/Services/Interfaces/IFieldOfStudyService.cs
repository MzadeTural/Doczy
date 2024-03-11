using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.FieldOfStudyDtos;

namespace Doczy.Business.Services.Interfaces
{
	public interface IFieldOfStudyService
	{
		Task<ResponseDto> CreateFieldOfStudyAsync(CreateFieldOfStudyDto createFieldOfStudy);
		Task<ResponseDto> UpdateFieldOfStudy(Guid id,UpdateFieldOfStudyDto updateFieldOfStudy);
		Task<ResponseDto> DeleteFieldOfStudy(Guid id);
		Task<List<GetFieldOfStudyDto>> GetAllFieldOfStudiesAsync();
		Task<GetFieldOfStudyDto> GetFieldOfStudyAsync(Guid id);
    }
}

