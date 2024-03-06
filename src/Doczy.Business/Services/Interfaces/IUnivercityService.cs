using System;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UnivercityDtos;

namespace Doczy.Business.Services.Interfaces
{
	public interface IUnivercityService
	{
        Task<ResponseDto> CreateUnivercityAsync(CreateUnivercityDto model);
        Task<ResponseDto> UpdateUnivercityAsync(Guid id, UpdateUnivercityDto model);
        Task<ResponseDto> DeleteUnivercityAsync(Guid id);
    }
}

