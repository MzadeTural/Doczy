using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UserDtos;

namespace Doczy.Business.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<ResponseDto> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto);
    }
}
