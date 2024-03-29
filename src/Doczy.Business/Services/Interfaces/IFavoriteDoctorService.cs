using Doczy.Business.DTOs.Common;

namespace Doczy.Business.Services.Interfaces
{
    public interface IFavoriteDoctorService
    {
        Task<ResponseDto> CreateFavoriteDoctorAsync(Guid doctorId);
        Task<ResponseDto> RemoveFavoriteDoctorAsync(Guid doctorId);
    }
}
