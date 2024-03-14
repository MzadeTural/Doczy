using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.AppointmentDto;
using Doczy.Business.DTOs.AppointmentDtos;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.Experiance;
using Doczy.Business.Exceptions.ServiceExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Repositories.Implementations;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<BaseAppUser> _userManager;
        private readonly IAvailableHourRepository _availableHourRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<BaseAppUser> userManager, IServiceRepository serviceRepository, IAvailableHourRepository availableHourRepository, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _serviceRepository = serviceRepository;
            _availableHourRepository = availableHourRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseDto> CreateAppointmentAsync(CreateAppointmentDto model)
        {
            var userId = (await _userManager.GetUserAsync(_contextAccessor?.HttpContext?.User)).Id;
           var existService= await _serviceRepository.IsExistAsync(s => s.Id == model.ServiceId);
            if (!existService)
                throw new ServiceNotFoundException("Service not found");
            var time = await _availableHourRepository.GetByIdAsync(model.ChosenHourId);
            Appointment newAppointment = _mapper.Map<Appointment>(model);
            newAppointment.PatientId = userId;
            newAppointment.AppointmentTime = time.Time;
            var result = await _appointmentRepository.CreateAsync(newAppointment);
            await _appointmentRepository.SaveAsync();

            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Appointment successfully created" : "Something went wrong"
                         );


        }

        public async Task<List<GetAppointmentDto>> GetAppointmentAsync()
        {
            var doctorId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;

            ArgumentNullException.ThrowIfNull(doctorId);

            var appointment = await _appointmentRepository.FindAll(c => c.DoctorId == doctorId,
                                                                    tracking: false,
                                                                    a => a.Service,
                                                                    a => a.Service.ServiceType,
                                                                    a => a.Patient)
                                                                    .ProjectTo<GetAppointmentDto>(_mapper.ConfigurationProvider)
                                                                    .ToListAsync();


          

            return appointment;
        }
    }
}
