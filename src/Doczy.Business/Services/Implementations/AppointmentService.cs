using AutoMapper;
using AutoMapper.QueryableExtensions;
using Doczy.Business.DTOs.AppointmentDto;
using Doczy.Business.DTOs.AppointmentDtos;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.PaymentDtos;
using Doczy.Business.Exceptions.DoctorAvailabilityExceptions;
using Doczy.Business.Exceptions.ServiceExceptions;
using Doczy.Business.Exceptions.TemporaryAppointmentExceptions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Migrations;
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
        private readonly IPaymentService _paymentService;
        private readonly ITempAppointmentRepository _tempAppointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<BaseAppUser> userManager, IServiceRepository serviceRepository, IAvailableHourRepository availableHourRepository, IHttpContextAccessor httpContextAccessor, IPaymentService paymantService, ITempAppointmentRepository tempAppointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _serviceRepository = serviceRepository;
            _availableHourRepository = availableHourRepository;
            _httpContextAccessor = httpContextAccessor;
            _paymentService = paymantService;
            _tempAppointmentRepository = tempAppointmentRepository;

        }


        public async Task<ResponseDto> CreateAppointmentAsync(CreateAppointmentDto model)
        {
            var userId = (await _userManager.GetUserAsync(_contextAccessor?.HttpContext?.User)).Id;
            var existService = await _serviceRepository.IsExistAsync(s => s.Id == model.ServiceId && s.DoctorId==model.DoctorId);
            if (!existService)
                throw new ServiceNotFoundException();

            var time = await _availableHourRepository.GetByIdAsync(model.ChosenHourId);
            if (time is null)
                throw new TimeNotFoundByIdException(model.ChosenHourId);
            var service = await _serviceRepository.GetByIdAsync(model.ServiceId);
           
            var amount = service.Price;
            // Calculate payment amount based on service or any other relevant factors
            decimal paymentAmount = _paymentService.CalculatePaymentAmount(model);

            // Prepare payment request body
            var paymentRequestBody = new
            {
                amount = paymentAmount,
                approveURL = "https://localhost:7046/api/Paymant/callback",
                cancelURL = "your_cancel_url",
                declineURL = "your_decline_url",
                currencyType = "AZN",
                description = "Appointment payment",
                language = "AZ",
                senderCardUID = userId.ToString()
            };

            // Make createOrder request to initiate payment
            var paymentResponse = await _paymentService.MakePaymentRequestAsync("createOrder", amount, "Appointment payment");

            // Check if payment initiation was successful
            if (paymentResponse.IsSuccessStatusCode)
            {
                // Parse paymentUrl from response
                var parsePaymentResponse = _paymentService.ParsePaymentDataFromResponse(paymentResponse);
                var paymentUrl = parsePaymentResponse.Payload.PaymentUrl;



                var newTempAppointment = _mapper.Map<TempAppointment>(model);
                newTempAppointment.PatientId = userId;
                newTempAppointment.AppointmentTime = time.Time;
                newTempAppointment.PaymentUrl = paymentUrl;
                newTempAppointment.PaymentAmount = amount;
                newTempAppointment.SessionId = parsePaymentResponse.Payload.SessionId;
                newTempAppointment.OrderId = parsePaymentResponse.Payload.OrderId;

                var result = await _tempAppointmentRepository.CreateAsync(newTempAppointment);
                await _tempAppointmentRepository.SaveAsync();

                return new ResponseDto(
                    StatusCode: result ? HttpStatusCode.Created : HttpStatusCode.BadRequest,
                    Message: result ? $"{paymentUrl}" : "Something went wrong"
                );
            }
            else
            {
                return new ResponseDto(
                    StatusCode: HttpStatusCode.InternalServerError,
                    Message: "Failed to initiate payment"
                );
            }
        }
        public async Task<List<GetDoctorAppointmentDto>> GetDoctorAppointmentAsync()
        {
            var doctorId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            ArgumentNullException.ThrowIfNull(doctorId);

            var appointment = await _appointmentRepository.FindAll(c => c.DoctorId == doctorId,
                                                                    tracking: false,
                                                                    a => a.Service,
                                                                    a => a.Service.ServiceType,
                                                                    a => a.Patient)
                                                                    .ProjectTo<GetDoctorAppointmentDto>(_mapper.ConfigurationProvider)
                                                                    .ToListAsync();

            return appointment;
        }

        public async Task<List<GetPatientAppointmentDto>> GetPatientAppointmentAsync()
        {
            var patientId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            ArgumentNullException.ThrowIfNull(patientId);

            var appointment = await _appointmentRepository.FindAll(c => c.PatientId == patientId,
                                                                    tracking: false,
                                                                    a => a.Service,
                                                                    a => a.Service.ServiceType,
                                                                    a => a.Doctor)
                                                                    .ProjectTo<GetPatientAppointmentDto>(_mapper.ConfigurationProvider)
                                                                    .ToListAsync();

            return appointment;
        }






        public async Task CleanupTemporaryAppointmentData()
        {
            var userId = (await _userManager.GetUserAsync(_contextAccessor?.HttpContext?.User)).Id;
            // Clean up temporary appointment data for the given user
            var tempAppointments = await _appointmentRepository.FindAll(a => a.PatientId == userId).ToListAsync();
            foreach (var tempAppointment in tempAppointments)
            {
                _appointmentRepository.Delete(tempAppointment);
            }
            await _appointmentRepository.SaveAsync();
        }

        public async Task<ResponseDto> UpdateAppointmentPaymentStatusAsync(CallbackData paymentCallback)
        {
            var tempAppointment = await _tempAppointmentRepository.GetSingleAysnc(ta => ta.OrderId == paymentCallback.Payload.OrderId && ta.SessionId == paymentCallback.Payload.SessionId && !ta.IsDeleted);

            if (tempAppointment is null)
                throw new TemporaryAppointmentNotFoundException();


            // Check the payment status from the callback
            if (paymentCallback.Payload.OrderStatus == "APPROVED")
            {
                // Payment was successful, create the appointment
                var newAppointment = _mapper.Map<Appointment>(tempAppointment);

                await _appointmentRepository.CreateAsync(newAppointment);
                await _appointmentRepository.SaveAsync();
            }
            else
            {
                // Payment was declined or canceled, handle accordingly
                // For example, you can log the payment status or perform any necessary cleanup
                // You might also want to notify the user about the payment failure
                // For now, let's just log a message
                return new ResponseDto(
                   StatusCode: HttpStatusCode.InternalServerError,
                   Message: $"Payment for appointment {tempAppointment.Id} failed: {paymentCallback.Payload.ResponseDescription}"
               );
            }

            _tempAppointmentRepository.SoftDelete(tempAppointment);
            await _tempAppointmentRepository.SaveAsync();
            return new ResponseDto(
                   StatusCode: HttpStatusCode.Created,
                   Message: "Appointment successfully created and paid"
               );

        }


    }
}
