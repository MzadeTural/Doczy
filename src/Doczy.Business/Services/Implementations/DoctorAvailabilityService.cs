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
        private readonly IAvailableHourRepository _availableHourRepository;

        public DoctorAvailabilityService(IDoctorAvailabilityRepository doctorAvailabilityRepository, UserManager<BaseAppUser> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper, IAvailableHourRepository availableHourRepository)
        {
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _availableHourRepository = availableHourRepository;
        }

        public async Task<ResponseDto> CreateDoctorAvailabilityAsync(CreateDoctorAvailabilityDto model)
        {
            bool result = true;
            var doctorId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            if (model.AvailableHours == null || model.AvailableHours.Count == 0)
                throw new ArgumentException("At least one available hour must be specified.");
            var existingAvailability = await _doctorAvailabilityRepository.GetSingleAysnc(da => da.DoctorId == doctorId && da.DayOfWeek == model.DayOfWeek);
            List<AvailableHour> AvailableHours = new List<AvailableHour>();
            foreach (var availableHourDto in model.AvailableHours)
            {
                AvailableHours.Add(new AvailableHour
                {
                    Time = new TimeSpan(availableHourDto.Hour, availableHourDto.Minute, 0),
                });
            }
            if (existingAvailability != null)
                existingAvailability.AvailableHours = AvailableHours;
            else
            {
                var doctorAvailability = _mapper.Map<DoctorAvailability>(model);
                doctorAvailability.DoctorId = doctorId;
                result = await _doctorAvailabilityRepository.CreateAsync(doctorAvailability);

            }

            await _doctorAvailabilityRepository.SaveAsync();
            return new ResponseDto(
                        StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                        Message: result ? "DoctorAvailability  successfully created" : "Something went wrong"
                        );
        }

        public async Task<ResponseDto> DeleteDoctorAvailability(Guid id)
        {
            var doctorAvailability = await _doctorAvailabilityRepository.GetSingleAysnc(da=>da.Id==id,d=>d.AvailableHours);
            var result = false;
          var hourResponse=  _availableHourRepository.DeleteRange(doctorAvailability.AvailableHours);
          var dayResponese=  _doctorAvailabilityRepository.Delete(doctorAvailability);
            if (hourResponse && dayResponese)
                result = true;
            await _doctorAvailabilityRepository.SaveAsync();
            return new ResponseDto(
                          StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                          Message: result ? "DoctorAvailability successfully deleted" : "Something went wrong"
                          );
        }
    }
}
