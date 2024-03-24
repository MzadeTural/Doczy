using Doczy.Business.DTOs.AuthDtos;
using Doczy.Core.Entities.Identities;

namespace Doczy.Business.Services.Interfaces
{

    public interface ITokenHandler
    {
        Task<TokenResponseDto> CreateAccessTokenAsync(int second, BaseAppUser user);
        string CreateRefreshToken();
    }
}
