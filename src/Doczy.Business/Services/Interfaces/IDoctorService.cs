using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Core.Entities;

namespace Doczy.Business.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<List<GetDoctorAppointmentsDto>> GetDoctorAppointments(Guid userId);
    }
}
