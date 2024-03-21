using AutoMapper;
using Doczy.Business.DTOs.AvailableHoursDtos;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorAvailabilityDtos;
using Doczy.Business.Enums;
using Doczy.Business.Exceptions.DoctorAvailabilityExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Repositories.Implementations;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
        private readonly IAppointmentRepository _appointmentRepository;

        public DoctorAvailabilityService(IDoctorAvailabilityRepository doctorAvailabilityRepository, UserManager<BaseAppUser> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper, IAvailableHourRepository availableHourRepository, IAppointmentRepository appointmentRepository)
        {
            _doctorAvailabilityRepository = doctorAvailabilityRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _availableHourRepository = availableHourRepository;
            _appointmentRepository = appointmentRepository;
        }

        public async Task<ResponseDto> CreateDoctorAvailabilityAsync(CreateDoctorAvailabilityDto model)
        {
            bool result = true;
            var doctorId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            if (model.AvailableHours == null || model.AvailableHours.Count == 0)
                throw new ArgumentException("At least one available hour must be specified.");
            var existingAvailability = await _doctorAvailabilityRepository.GetSingleAysnc(da => da.DoctorId == doctorId && da.DayOfWeek == model.DayOfWeek);
            var existTime = await _availableHourRepository.FindAll(da => da.DoctorAvailabilityId == existingAvailability.Id).ToListAsync();
            List<AvailableHour> AvailableHours = new List<AvailableHour>();

            foreach (var availableHourDto in model.AvailableHours)
            {
                var time = new TimeSpan(availableHourDto.Hour, availableHourDto.Minute, 0);
                bool exist = false;
                foreach (var hour in existTime)
                {
                    if (hour.Time == time)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    AvailableHours.Add(new AvailableHour
                    {
                        Time = time
                    });
                }


            }
                existTime.AddRange(AvailableHours);
            if (existingAvailability != null)
                existingAvailability.AvailableHours = existTime;
            else
            {
                var doctorAvailability = _mapper.Map<DoctorAvailability>(model);
                doctorAvailability.AvailableHours= existTime;
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
            var doctorAvailability = await _doctorAvailabilityRepository.GetSingleAysnc(da => da.Id == id, d => d.AvailableHours);
            var result = false;
            var hourResponse = _availableHourRepository.DeleteRange(doctorAvailability.AvailableHours);
            var dayResponese = _doctorAvailabilityRepository.Delete(doctorAvailability);
            if (hourResponse && dayResponese)
                result = true;
            await _doctorAvailabilityRepository.SaveAsync();
            return new ResponseDto(
                          StatusCode: result ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                          Message: result ? "DoctorAvailability successfully deleted" : "Something went wrong"
                          );
        }

        public async Task<List<GetDoctorAvailabilityDto>> GetDoctorOwnAvailabilityAsync()
        {
            var doctorId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            var doctorAvailability = await _doctorAvailabilityRepository
           .FindAll(da => da.DoctorId == doctorId, tracking: false, da => da.AvailableHours).ToListAsync();

            var result = new List<GetDoctorAvailabilityDto>();
            foreach(DayOfWeek dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
        {
                var availabilityForDay = doctorAvailability.FirstOrDefault(da => da.DayOfWeek == dayOfWeek);

                if (availabilityForDay != null)
                {
                    // Map entity to DTO
                    var availabilityDto = _mapper.Map<GetDoctorAvailabilityDto>(availabilityForDay);
                    result.Add(availabilityDto);
                }
                else
                {
                    // If availability for this day is not defined, create DTO with an empty list of available hours
                    result.Add(new GetDoctorAvailabilityDto
                    {
                        DayOfWeek = dayOfWeek,
                        AvailableHours = new List<GetAvailableHourDto>()
                    });
                }
            }

            return result;

           
        }



        public async Task<GetDoctorAvailabilityDto> GetDoctorAvailabilityAsync(Guid doctorId, DateTime date)
        {
            ArgumentNullException.ThrowIfNull(doctorId);
            var dayOfWeek = date.DayOfWeek;
            var doctorAvailability = await _doctorAvailabilityRepository
                .GetSingleAysnc(da => da.DoctorId == doctorId && da.DayOfWeek == dayOfWeek, da => da.AvailableHours);

            if (doctorAvailability is null)
                throw new DoctorAvailabilityNotFoundException();

            // Fetch appointments for this doctor and date
            var appointments = _appointmentRepository
                .FindAll(a => a.DoctorId == doctorId &&
                            a.AppointmentDate.Date == date.Date && !a.IsDeleted) 
                .Select(a => a.AppointmentTime)
                .ToList();

            // Exclude appointment hours
            //var availableHours = doctorAvailability.AvailableHours
            //    .Select(ah => ah.Time)
            //    .Except(appointments)
            //    .ToList();
            var availableHours = doctorAvailability.AvailableHours
                .Where(ah => !appointments.Contains(ah.Time)) // Filter out occupied hours
                .Select(ah => _mapper.Map<GetAvailableHourDto>(ah))
                .ToList();

            var availabilityDto = _mapper.Map<GetDoctorAvailabilityDto>(doctorAvailability);
            //availabilityDto.AvailableHours = availableHours.Select(time => new GetAvailableHourDto { Time = time }).ToList();
            availabilityDto.AvailableHours = availableHours;
            return availabilityDto;

        }


    }
}

