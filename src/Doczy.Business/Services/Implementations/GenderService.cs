using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.GenderDtos;
using Doczy.Business.Exceptions.GenderExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class GenderService : IGenderService
    {
        private readonly IMapper _mapper;
        private readonly IGenderRepository _genderRepository;
        public GenderService(IMapper mapper, IGenderRepository genderRepository)
        {
            _mapper = mapper;
            _genderRepository = genderRepository;
        }

        public async Task<ResponseDto> CreateGender(CreateGenderDto model)
        {
            var isExist = await _genderRepository.IsExistAsync(g => g.Name == model.Name);
            if (isExist)
                throw new GenderAlreadyExistException($"{model.Name} already exist");
            var gender = _mapper.Map<Gender>(model);
            var result= await _genderRepository.CreateAsync(gender);
            await _genderRepository.SaveAsync();
            return new ResponseDto(
                        StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                        Message: result ? "Gender successfully created" : "Something went wrong"
                        );

        }

        public async Task<List<GetGenderDto>> GetGenders()
        {
            var genders = await _genderRepository
                                        .FindAll(g => !g.IsDeleted, tracking: false)
                                        .ProjectTo<GetGenderDto>(_mapper.ConfigurationProvider)
                                        .ToListAsync();
            return genders;
        }
    }
}
