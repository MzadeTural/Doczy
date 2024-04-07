using Doczy.Business.DTOs.AuthDtos;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UserDtos;
using Microsoft.AspNetCore.Mvc;

namespace Doczy.Business.Services.Interfaces
{
    public interface IAuthService
    {
         Task<LoginResponseDto> LoginAsync(LoginDto model, int accessTokenLifeTime);
          Task LogOutAsync();
        Task<ResponseDto> VerifyOTPAsync(VerifyOTPDto model);
         Task<TokenResponseDto> RefreshTokenLoginAsync(string refreshToken);
         Task<ResponseDto> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto);
        Task<ResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto model);
        Task<ResponseDto> ConfirmOTPAsync(ConfirmOTPDto model);
        Task<ResponseDto> ResetPasswordAsync(ResetPasswordDto model);
        Task<ResponseDto> VerifiedDoctorAsync(Guid doctorId);
    }
}
