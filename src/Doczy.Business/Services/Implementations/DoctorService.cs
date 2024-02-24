using Doczy.Business.DTOs.DoctorDtos;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities;

namespace Doczy.Business.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        public Task<List<GetDoctorAppointmentsDto>> GetDoctorAppointments(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
