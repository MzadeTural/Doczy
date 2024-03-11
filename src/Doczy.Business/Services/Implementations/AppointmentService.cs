using AutoMapper;
using Doczy.Business.DTOs.AppointmentDto;
using Doczy.Business.DTOs.Common;
using Doczy.Business.Exceptions.ServiceExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<BaseAppUser> userManager, IServiceRepository serviceRepository)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _serviceRepository = serviceRepository;
        }

        public async Task<ResponseDto> CreateAppointmentAsync(CreateAppointmentDto model)
        {
            var userId = (await _userManager.GetUserAsync(_contextAccessor?.HttpContext?.User)).Id;
           var existService= await _serviceRepository.IsExistAsync(s => s.Id == model.ServiceId);
            if (!existService)
                throw new ServiceNotFoundException("Service not found");
            Appointment newExperiance = _mapper.Map<Appointment>(model);
            newExperiance.PatientId = userId;
            var result = await _appointmentRepository.CreateAsync(newExperiance);
            await _appointmentRepository.SaveAsync();

            return new ResponseDto(
                         StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                         Message: result ? "Appointment successfully created" : "Something went wrong"
                         );


        }
    }
}
