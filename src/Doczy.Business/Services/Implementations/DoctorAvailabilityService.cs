using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorAvailabilityDtos;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class DoctorAvailabilityService : IDoctorAvailabilityService
    {
        private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        private readonly UserManager<BaseAppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public DoctorAvailabilityService(IDoctorAvailabilityRepository doctorAvailabilityRepository, UserManager<BaseAppUser> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<ResponseDto> CreateDoctorAvailabilityAsync(CreateDoctorAvailabilityDto model)
        {
            bool result=true;
            var doctorId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            if (model.AvailableHours == null || model.AvailableHours.Count == 0)
                throw new ArgumentException("At least one available hour must be specified.");

            
            var existingAvailability= await _doctorAvailabilityRepository.GetSingleAysnc(da => da.DoctorId == doctorId && da.DayOfWeek == model.DayOfWeek);
            if (existingAvailability != null)
                existingAvailability.AvailableHours = model.AvailableHours;
            else
            {
                var newḊoctorAvailability = _mapper.Map<DoctorAvailability>(model);
                newḊoctorAvailability.DoctorId = doctorId;
                 result = await _doctorAvailabilityRepository.CreateAsync(newḊoctorAvailability);
            }
           await _doctorAvailabilityRepository.SaveAsync();
            return new ResponseDto(
                        StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                        Message: result ? "DoctorAvailability  successfully created" : "Something went wrong"
                        );
        }
    }
}
