using Doczy.Business.DTOs;
using Doczy.Business.DTOs.Common;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities.Identities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using System;

namespace Doczy.Business.Services.Implementations
{
    public class UserService:IUserService
    {
        private readonly UserManager<AppUser> _userManager;     
        private readonly IHttpContextAccessor _httpContextAccessor;
       





        public UserService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
          
        }



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
