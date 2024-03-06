using System;
using System.Net;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UnivercityDtos;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;

namespace Doczy.Business.Services.Implementations
{
	public class UnivercityService:IUnivercityService
	{

        public async Task<ResponseDto> CreateUnivercityAsync(CreateUnivercityDto model)
        {
            DoctorCategory newCategory = _mapper.Map<DoctorCategory>(model);
            var result = await _doctorCategoryRepository.CreateAsync(newCategory);
            await _doctorCategoryRepository.SaveAsync();

            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Category  successfully created" : "Something went wrong"
                         );
        }

        public Task<ResponseDto> CreateUnivercityAsync(Guid id, UpdateUnivercityDto model)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> CreateUnivercityAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

