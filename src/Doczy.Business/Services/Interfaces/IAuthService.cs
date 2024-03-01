using Doczy.Business.DTOs.AuthDtos;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UserDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IAuthService
    {
         Task<LoginResponseDto> LoginAsync(LoginDto model, int accessTokenLifeTime);
         Task<TokenResponseDto> RefreshTokenLoginAsync(string refreshToken);
         Task<ResponseDto> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto);
    }
}
