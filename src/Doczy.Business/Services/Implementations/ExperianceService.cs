using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.Experiance;
using Doczy.Business.Exceptions.ExperianceExceptions;
using Doczy.Business.Exceptions.HospitalExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Repositories.Implementations;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class ExperianceService : IExperianceService
    {
        private readonly IExperianceRepository _experianceRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<BaseAppUser> _userManager;
        private readonly IHospitalRepository _hospitalRepository;
        public ExperianceService(IExperianceRepository experianceRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, UserManager<BaseAppUser> userManager, IHospitalRepository hospitalRepository)
        {
            _experianceRepository = experianceRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
            _hospitalRepository = hospitalRepository;
        }

        public async Task<ResponseDto> CreateExperianceAsync(CreateExperianceDto model)
        {
            var doctorId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            Experiance newExperiance = _mapper.Map<Experiance>(model);
            newExperiance.DoctorId = doctorId;
            if (model.currentlyWorking)
                newExperiance.EndDate = null;
            var result = await _experianceRepository.CreateAsync(newExperiance);
            await _experianceRepository.SaveAsync();

            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Experiance  successfully created" : "Something went wrong"
                         );

        }

        public async Task<List<GetExperianceDto>> GetExperiancesAsync()
        {
            var doctorId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            ArgumentNullException.ThrowIfNull(doctorId);

            var experiances = await _experianceRepository.FindAll(c => c.DoctorId == doctorId, tracking: false, e => e.Hospital)
                                                            .ProjectTo<GetExperianceDto>(_mapper.ConfigurationProvider)
                                                             .ToListAsync();
            return experiances;

        }

        public async Task<ResponseDto> UpdateExperianceAsync(Guid experianceId, UpdateExperianceDto model)
        {
            var dbExperience = await _experianceRepository.GetSingleAysnc(e => e.Id == experianceId && !e.IsDeleted);
            var IsExist = await _hospitalRepository.IsExistAsync(e => e.Id == model.HospitalId && !e.IsDeleted);
            if (dbExperience is null)
                throw new ExperianceNotFoundException();
            if (!IsExist)
                throw new HospitalNotFoundByIdException(model.HospitalId);

            dbExperience.Title = model.Title ?? dbExperience.Title;
            dbExperience.Location = model.Location ?? dbExperience.Location;
            dbExperience.HospitalId = model?.HospitalId != null ? model.HospitalId : dbExperience.HospitalId;
            dbExperience.StartDate = model?.StartDate != null ? model.StartDate : dbExperience.StartDate;
            if (!model.currentlyWorking)
                dbExperience.EndDate = model?.EndDate != null ? model.EndDate : dbExperience.EndDate;
            dbExperience.currentlyWorking = model.currentlyWorking;
            var result = _experianceRepository.Update(dbExperience);
            await _experianceRepository.SaveAsync();
            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.NoContent : HttpStatusCode.BadRequest,
                         Message: result ? "Experiance successfully updated" : "Something went wrong"
                         );



        }
    }
}
