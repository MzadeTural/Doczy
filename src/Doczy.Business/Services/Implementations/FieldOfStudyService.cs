using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.FieldOfStudyDtos;
using Doczy.Business.Exceptions.FieldOfStudyExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Doczy.Business.Services.Implementations
{
    public class FieldOfStudyService : IFieldOfStudyService
	{
        private IFieldOfStudyRepository _fieldOfStudyRepository;
        private IMapper _mapper;

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

        public async Task<ResponseDto> CreateFieldOfStudyAsync(CreateFieldOfStudyDto createFieldOfStudy)
        {
            var dbFields = await _fieldOfStudyRepository.FindAll(x => !x.IsDeleted).ToListAsync();
            var isExist = dbFields.Any(x => x.Name.Trim().ToLower() == createFieldOfStudy.Name.Trim().ToLower());
            if (isExist) throw new FieldOfStudyAlreadyExistExceptions("Field is already exist");

            throw new NotImplementedException();
        }

        public Task<ResponseDto> UpdateFieldOfStudy(Guid id, UpdateFieldOfStudyDto updateFieldOfStudy)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> DeleteFieldOfStudy(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}

