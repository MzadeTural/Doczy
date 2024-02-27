using Doczy.Business.DTOs.AuthDtos;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UserDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<LoginResponseDto> LoginAsync(LoginDto model, int accessTokenLifeTime);
        public Task<TokenResponseDto> RefreshTokenLoginAsync(string refreshToken);
        public Task<ResponseDto> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto);
    }
}
