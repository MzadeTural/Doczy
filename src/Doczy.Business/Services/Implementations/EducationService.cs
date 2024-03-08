using System.Net;
using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.EducationDtos;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.DataAccess.Repositories.Interfaces;

namespace Doczy.Business.Services.Implementations
{
    public class EducationService : IEducationService
	{
        private IEducationRepository _educationRepository;
        private IMapper _mapper;

        public EducationService(IEducationRepository educationRepository,IMapper mapper)
        {
            _educationRepository = educationRepository;
            _mapper = mapper;
        }
        public Task<EducationDto> GetEducation(Guid DoctorId)
        {
            throw new NotImplementedException();
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
    }
}

