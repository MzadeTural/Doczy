using System;
using System.Net;
using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UnivercityDegreeDtos;
using Doczy.Business.Exceptions.UnivercityDegreeExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Doczy.Business.Services.Implementations
{
	public class UnivercityDegreeService : IUnivercityDegreeService
    {
        private IUnivercityDegreeRepository _univercityDegreeRepository;
        private IMapper _mapper;

        public UnivercityDegreeService(IUnivercityDegreeRepository univercityDegreeRepository,IMapper mapper)
		{
            _univercityDegreeRepository = univercityDegreeRepository;
            _mapper = mapper;
        }

        public async Task<GetUnivercityDegreeDto> GetUnivercityDegreeAsync(Guid id)
        {
            var field = await _univercityDegreeRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if (field is null) throw new UnivercityDegreeNotFoundExceptions("Degree is not found");
            var model = _mapper.Map<GetUnivercityDegreeDto>(field);
            return model;
        }

        public async Task<List<GetUnivercityDegreeDto>> GetAllUnivercityDegreesAsync()
        {
            var dbFields = await _univercityDegreeRepository.FindAll(x => !x.IsDeleted).ToListAsync();
            return _mapper.Map<List<GetUnivercityDegreeDto>>(dbFields);
        }

        public async Task<ResponseDto> CreateUnivercityDegreeAsync(CreateUnivercityDegreeDto createUnivercityDegree)
        {
            var dbFields = await _univercityDegreeRepository.FindAll(x => !x.IsDeleted).ToListAsync();
            var isExist = dbFields.Any(x => x.Name.Trim().ToLower() ==
                                            createUnivercityDegree.Name.Trim().ToLower());

            if (isExist) throw new UnivercityDegreeAlreadyExistExceptions("Degree is already exist");
            var newField = _mapper.Map<UnivercityDegree>(createUnivercityDegree);
            var result = await _univercityDegreeRepository.CreateAsync(newField);
            await _univercityDegreeRepository.SaveAsync();
            return new ResponseDto(
                StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                Message: result ? "Degree successfully created" : "Something went wrong");
        }

        public async Task<ResponseDto> UpdateUnivercityDegree(Guid id, UpdateUnivercityDegreeDto updateUnivercityDegree)
        {
            var dbField = await _univercityDegreeRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if (dbField is null) throw new UnivercityDegreeNotFoundExceptions("Degree is not found");

            if (updateUnivercityDegree.Name != null)
            {
                dbField.Name = updateUnivercityDegree.Name;
            }
            var result = _univercityDegreeRepository.Update(dbField);
            await _univercityDegreeRepository.SaveAsync();
            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                         Message: result ? "Degree successfully updated" : "Something went wrong"
                         );
        }

        public async Task<ResponseDto> DeleteUnivercityDegree(Guid id)
        {
            var dbField = await _univercityDegreeRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if (dbField is null) throw new UnivercityDegreeNotFoundExceptions("Degree is not found");
            var result = _univercityDegreeRepository.SoftDelete(dbField);
            await _univercityDegreeRepository.SaveAsync();
            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                         Message: result ? "Degree successfully deleted" : "Something went wrong"
                         );
        }

    }
}

