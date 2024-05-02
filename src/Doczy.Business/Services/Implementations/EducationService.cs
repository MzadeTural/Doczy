using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.EducationDtos;
using Doczy.Business.Exceptions.EducationExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Doczy.Business.Services.Implementations
{
    public class EducationService : IEducationService
	{
        private readonly IEducationRepository _educationRepository;
        private readonly IMapper _mapper;

        public EducationService(IEducationRepository educationRepository,IMapper mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }
        public async Task<List<EducationDto>> GetAllEducationsAsync(Guid doctorId)
        {
           var model = await _educationRepository.FindAll(x => x.DoctorId == doctorId && !x.IsDeleted,
                                                                      tracking:false,
                                                                      c=>c.Univercity,
                                                                      c=>c.FieldOfStudy,
                                                                      c=>c.UnivercityDegree).ProjectTo<EducationDto>(_mapper.ConfigurationProvider)
                                                                      .ToListAsync();
            if(model is null) throw new EducationNotFoundException("Not Found Education");
            else return model;
        }
        public async Task<EducationDto> GetEducationAsync(Guid id)
        {
            var model = await _educationRepository.FindAll(x => x.Id == id && !x.IsDeleted,
                                                                       tracking: false,
                                                                       c => c.Univercity,
                                                                       c => c.FieldOfStudy,
                                                                       c => c.UnivercityDegree).ProjectTo<EducationDto>(_mapper.ConfigurationProvider)
                                                                       .FirstOrDefaultAsync();
            if (model is null) throw new EducationNotFoundException("Not Found Education");
            else return model;
        }

        public async  Task<ResponseDto> CreateEducationAsync(CreateEducationDto model)
        {
            var newEducation = _mapper.Map<Education>(model);
            var result = await _educationRepository.CreateAsync(newEducation);
            await _educationRepository.SaveAsync();
            return new ResponseDto(
                StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                Message: result ? "Education successfully created" : "Something went wrong");

        }
        public async Task<ResponseDto> UpdateEducation(Guid id, UpdateEducationDto model)
        {
            var dbEducation = await _educationRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if (dbEducation is null) throw new EducationNotFoundException("Not Found Education");
            //dbEducation = _mapper.Map<Education>(model);
            if (model.UnivercityId != null)
            {
                dbEducation.UnivercityId = model.UnivercityId;
            }
            if (model.UnivercityDegreeId != null)
            {
                dbEducation.UnivercityDegreeId = model.UnivercityDegreeId;
            }
            if (model.FieldOfStudyId != null)
            {
                dbEducation.FieldOfStudyId = model.FieldOfStudyId;
            }
            if (model.StartDate != null)
            {
                dbEducation.StartDate = model.StartDate;
            }
            if (model.EndDate != null)
            {
                dbEducation.EndDate = model.EndDate;
            }
            var result = _educationRepository.Update(dbEducation);
            await _educationRepository.SaveAsync();
            return new ResponseDto(
                StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                Message: result ? "Education successfully Updated" : "Something went wrong");

        }
        public async Task<ResponseDto> DeleteEducation(Guid id)
        {
            var dbEducation = await _educationRepository.GetSingleAysnc(x => x.Id == id && !x.IsDeleted);
            if (dbEducation is null) throw new EducationNotFoundException("Not Found Education");
            var result = _educationRepository.SoftDelete(dbEducation);
            await _educationRepository.SaveAsync();
            return new ResponseDto(
                StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                Message: result ? "Education successfully Deleted" : "Something went wrong");

        }
    }
}

