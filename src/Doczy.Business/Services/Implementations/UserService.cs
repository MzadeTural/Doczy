using Doczy.Business.DTOs;
using Doczy.Business.DTOs.Common;
using Doczy.Business.Enums;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities.Identities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;

namespace Doczy.Business.Services.Implementations
{
    public class UserService : IUserService
    {
      
        public Task<ResponseDto> CreateAsync(CreateUserDto model)
        {
            throw new NotImplementedException();
        }

        public Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenEndDate, int refreshTokenLifeTime)
        {
            throw new NotImplementedException();
        }
    }
}
