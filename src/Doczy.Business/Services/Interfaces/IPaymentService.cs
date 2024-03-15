using Doczy.Business.DTOs.AppointmentDto;
using Doczy.Business.DTOs.PaymentDtos;
using Doczy.Core.Entities;
using System.Threading.Tasks;

namespace Doczy.Business.Services.Interfaces
{
    public interface IPaymentService
    {

       // Task<HttpResponseMessage> MakePaymentRequestAsync(string endpoint, object requestBody);
        Task<HttpResponseMessage> MakePaymentRequestAsync(string endpoint,decimal amount ,string description);
        Task<string> InitiatePaymentAsync(double sumAmount, string desc);
        decimal CalculatePaymentAmount(CreateAppointmentDto model);
        PayriffResponseDto ParsePaymentDataFromResponse(HttpResponseMessage response);
        Task RedirectUserToPayment(string paymentUrl);
        Task HandlePaymentCallbackAsync(CallbackData paymentCallback);
    }
}
