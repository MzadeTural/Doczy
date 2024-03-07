using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.RaitingDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IDoctorRaitingService
    {
        Task<ResponseDto> CreateRaitingDoctor(CreateRaitingDto model);
    }
}
