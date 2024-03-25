using System.Net;
using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.FieldOfStudyDtos;
using Doczy.Business.Exceptions.FieldOfStudyExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Doczy.Business.Services.Implementations
{
    public class FieldOfStudyService : IFieldOfStudyService
	{
        private readonly IFieldOfStudyRepository _fieldOfStudyRepository;
        private readonly IMapper _mapper;

        public FieldOfStudyService(IFieldOfStudyRepository fieldOfStudyRepository, IMapper mapper)
		{
            _fieldOfStudyRepository = fieldOfStudyRepository;
            _mapper = mapper;
		}

        public async Task<List<GetFieldOfStudyDto>> GetAllFieldOfStudiesAsync()
        {
            var dbFields = await _fieldOfStudyRepository.FindAll(x => !x.IsDeleted).ToListAsync();
            return _mapper.Map<List<GetFieldOfStudyDto>>(dbFields);
        }

        public async Task<GetFieldOfStudyDto> GetFieldOfStudyAsync(Guid id)
        {
            var field = await _fieldOfStudyRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if (field is null) throw new FieldOfStudyNotFoundExceptions("Field is not found");
            var model = _mapper.Map<GetFieldOfStudyDto>(field);
            return model;
        }

        public async Task<ResponseDto> CreateFieldOfStudyAsync(CreateFieldOfStudyDto createFieldOfStudy)
        {
            var dbFields = await _fieldOfStudyRepository.FindAll(x => !x.IsDeleted).ToListAsync();
            var isExist = dbFields.Any(x => x.Name.Trim().ToLower() ==
                                            createFieldOfStudy.Name.Trim().ToLower());

            if (isExist) throw new FieldOfStudyAlreadyExistExceptions("Field is already exist");
            var newField = _mapper.Map<FieldOfStudy>(createFieldOfStudy);
            var result = await _fieldOfStudyRepository.CreateAsync(newField);
            await _fieldOfStudyRepository.SaveAsync();
            return new ResponseDto(
                StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                Message: result ? "Filed successfully created" : "Something went wrong");
        }

        public async Task<ResponseDto> UpdateFieldOfStudy(Guid id, UpdateFieldOfStudyDto updateFieldOfStudy)
        {
            var dbField = await _fieldOfStudyRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if (dbField is null) throw new FieldOfStudyNotFoundExceptions("Field is not found");

            if (updateFieldOfStudy.Name != null)
            {
                dbField.Name = updateFieldOfStudy.Name;
            }
            var result = _fieldOfStudyRepository.Update(dbField);
            await _fieldOfStudyRepository.SaveAsync();
            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                         Message: result ? "Field successfully updated" : "Something went wrong"
                         );
        }

        public async Task<ResponseDto> DeleteFieldOfStudy(Guid id)
        {
            var dbField = await _fieldOfStudyRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if (dbField is null) throw new FieldOfStudyNotFoundExceptions("Field is not found");
            var result = _fieldOfStudyRepository.SoftDelete(dbField);
            await _fieldOfStudyRepository.SaveAsync();
            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                         Message: result ? "Field successfully deleted" : "Something went wrong"
                         );
        }

    }
}

