using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.Experiance;
using Doczy.Business.DTOs.FavoriteDoctorDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IFavoriteDoctorService
    {
        Task<ResponseDto> CreateFavoriteDoctorAsync(CreateFavoriteDoctorDto model);
    }
}
