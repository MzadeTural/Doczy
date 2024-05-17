using Doczy.Business.DTOs.AuthDtos;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Doczy.Business.Services.Implementations
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<BaseAppUser > _userManager;

        public TokenHandler(UserManager<BaseAppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;

            _configuration = configuration;

        }

        public async Task<TokenResponseDto> CreateAccessTokenAsync(int minute, BaseAppUser user)
        {
            TokenResponseDto tokenResponseDto = new();

            var claims = new List<Claim>()
        {
            new(ClaimTypes.Name, user.UserName),
             new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        };

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            tokenResponseDto.Expiration = DateTime.Now.AddMinutes(minute);
            JwtSecurityToken jwtSecurityToken = new(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: tokenResponseDto.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: signingCredentials,
                claims: claims
                );

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            tokenResponseDto.AccessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);

            tokenResponseDto.RefreshToken = CreateRefreshToken();
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.WriteToken(jwtSecurityToken);
            return tokenResponseDto;
        }

      

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}
