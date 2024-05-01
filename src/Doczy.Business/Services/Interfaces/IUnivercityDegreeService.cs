using System;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UnivercityDegreeDtos;

namespace Doczy.Business.Services.Interfaces
{
	public interface IUnivercityDegreeService
	{
        Task<ResponseDto> CreateUnivercityDegreeAsync(CreateUnivercityDegreeDto createUnivercityDegree);
        Task<ResponseDto> UpdateUnivercityDegree(Guid id, UpdateUnivercityDegreeDto updateUnivercityDegree);
        Task<ResponseDto> DeleteUnivercityDegree(Guid id);
        Task<List<GetUnivercityDegreeDto>> GetAllUnivercityDegreesAsync();
        Task<GetUnivercityDegreeDto> GetUnivercityDegreeAsync(Guid id);
    }
}

