using System;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UnivercityDtos;

namespace Doczy.Business.Services.Interfaces
{
	public interface IUnivercityService
	{
        Task<List<GetUnivercityDto>> GetUnivercitiesAsync();
        Task<GetUnivercityDto> GetUnivercityAsync(Guid id);
        Task<ResponseDto> CreateUnivercityAsync(CreateUnivercityDto model);
        Task<ResponseDto> UpdateUnivercity(Guid id, UpdateUnivercityDto model);
        Task<ResponseDto> DeleteUnivercity(Guid id);
    }
}

