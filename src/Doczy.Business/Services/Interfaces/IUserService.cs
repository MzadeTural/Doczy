using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Core.Entities.Identities;

namespace Doczy.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDto> CreateAsync(CreateUserDto model);
        Task<ResponseDto> CreateDoctorAsync(CreateDoctorDto model);
        Task UpdateRefreshToken(string refreshToken, BaseAppUser user, DateTime accessTokenEndDate, int refreshTokenLifeTime);
    }
}
