using Doczy.Business.DTOs.AppointmentDto;
using Doczy.Business.DTOs.Common;

namespace Doczy.Business.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<ResponseDto> CreateAppointmentAsync(CreateAppointmentDto model);
    }
}
