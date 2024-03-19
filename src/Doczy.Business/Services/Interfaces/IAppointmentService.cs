using Doczy.Business.DTOs.AppointmentDto;
using Doczy.Business.DTOs.AppointmentDtos;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.PaymentDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<ResponseDto> CreateAppointmentAsync(CreateAppointmentDto model);
        Task<List<GetDoctorAppointmentDto>> GetDoctorAppointmentAsync();
        Task<List<GetPatientAppointmentDto>> GetPatientAppointmentAsync();
        Task<ResponseDto> UpdateAppointmentPaymentStatusAsync(CallbackData paymentCallback);
        Task CleanupTemporaryAppointmentData();
    }
}
