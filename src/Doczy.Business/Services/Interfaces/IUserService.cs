using Doczy.Business.DTOs;
using Doczy.Business.DTOs.Common;
using Doczy.Core.Entities.Identities;

namespace Doczy.Business.Services.Interfaces
{
    internal interface IUserService
    {
        Task<ResponseDto> CreateAsync(CreateUserDto model);
        Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenEndDate, int refreshTokenLifeTime);
    }
}
