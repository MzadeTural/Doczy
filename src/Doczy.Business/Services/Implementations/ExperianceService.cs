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

        public async Task<ResponseDto> UpdateExperianceAsync(Guid experianceId ,UpdateExperianceDto model)
        {
            var dbExperiance = await _experianceRepository.GetSingleAysnc(e => e.Id == experianceId && !e.IsDeleted);
            var IsExist = await _hospitalRepository.IsExistAsync(e => e.Id == model.HospitalId && !e.IsDeleted);
            if (dbExperiance is null)
                throw new ExperianceNotFoundException();
            if (IsExist)
                throw new HospitalNotFoundByIdException(model.HospitalId);

            dbExperiance.Title= model.Title is not null ? model.Title : dbExperiance.Title;

            dbExperiance.Location= model.Location is not null ? model.Location : dbExperiance.Location;
            dbExperiance.Location= model.Location is not null ? model.Location : dbExperiance.Location;
            dbExperiance.HospitalId= model.HospitalId !=null ? model.HospitalId : dbExperiance.HospitalId;
            dbExperiance.StartDate= model.StartDate !=null ? model.StartDate : dbExperiance.StartDate;
            if(!model.currentlyWorking)
            dbExperiance.EndDate= model.EndDate != null ? model.EndDate : dbExperiance.EndDate;

            dbExperiance.currentlyWorking = model.currentlyWorking;

            var result = _experianceRepository.Update(dbExperiance);
            await _experianceRepository.SaveAsync();
            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                         Message: result ? "Experiance successfully updated" : "Something went wrong"
                         );


           
        }
    }
}
