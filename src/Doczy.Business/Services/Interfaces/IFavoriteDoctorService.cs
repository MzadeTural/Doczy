using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.FavoriteDoctorDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IFavoriteDoctorService
    {
        Task<ResponseDto> CreateFavoriteDoctorAsync(Guid doctorId);
        Task<ResponseDto> RemoveFavoriteDoctorAsync(Guid doctorId);
        Task<GetFavouriteDoctorDto> GetFavoriteDoctorAsync(Guid doctorId);
    }
}
