using Doczy.Business.DTOs.AuthDtos;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Business.Exceptions.AuthExceptions;
using Doczy.Business.Exceptions.AuthExceptions.Token;
using Doczy.Business.Exceptions.UserExceprions;
using Doczy.Business.Helpers.Extensions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities.Identities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<BaseAppUser> _userManager;
        private readonly SignInManager<BaseAppUser> _signInManager;

        private readonly IMailService _mailService;
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(UserManager<BaseAppUser> userManager, IMailService mailService, IUserService userService, ITokenHandler tokenHandler, IHttpContextAccessor httpContextAccessor, SignInManager<BaseAppUser> signInManager)
        {
            _userManager = userManager;
            _mailService = mailService;
            _userService = userService;
            _tokenHandler = tokenHandler;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
        }



        public async Task<ResponseDto> ConfirmEmailAsync(ConfirmEmailDto confirmEmailDto)
        {
            confirmEmailDto.Token.ThrowIfNullOrWhiteSpace(message: "Token cannot be null");
            confirmEmailDto.Email.ThrowIfNullOrWhiteSpace(message: "Email cannot be null");

            var user = await _userManager.FindByEmailAsync(confirmEmailDto.Email);
            if (user is null)
                throw new UserNotFoundException($"User not found by email: {confirmEmailDto.Email}", HttpStatusCode.BadRequest);

            if (await _userManager.IsEmailConfirmedAsync(user))
                throw new EmailConfirmationException("This account already activated");

            var result = await _userManager.ConfirmEmailAsync(user, confirmEmailDto.Token);
            if (!result.Succeeded)
                throw new EmailConfirmationException(result.Errors);

            var requestResult = new ConfirmEmailResponseDto(true, $"User successfully activated. Username: {user.UserName}");

            return new ResponseDto
            (
                StatusCode: requestResult.IsSuccess ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                Message: requestResult.IsSuccess ? requestResult.Message : "Something went wrong"
            );
        }

        

       public async Task<LoginResponseDto> LoginAsync(LoginDto model, int accessTokenLifeTime)
        {
            var loginCheck = _httpContextAccessor?.HttpContext?.User?.Identity;
            if (loginCheck?.IsAuthenticated == true)
                throw new AlreadyAuthenticationException("You are already authenticated", HttpStatusCode.BadGateway);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                throw new AuthenticationFailException();

            if (!await _userManager.IsEmailConfirmedAsync(user))
                throw new EmailNotConfirmedException();

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded)
            {
                var tokenResponse = await GenerateJwtTokenAsync(user, accessTokenLifeTime);
                return new()
                {
                    TokenResponse = tokenResponse
                };
            }
            throw new AuthenticationFailException();
        }

       public async Task<TokenResponseDto> RefreshTokenLoginAsync(string refreshToken)
        {
            BaseAppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user is null)
                throw new UserNotFoundException("User not found");

            if (user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                return await GenerateJwtTokenAsync(user, 2);
            }

            throw new RefreshTokenExpiredException();
        }
        private async Task<TokenResponseDto> GenerateJwtTokenAsync(BaseAppUser user, int accessTokenLifeTime)
        {
            var tokenResponse = await _tokenHandler.CreateAccessTokenAsync(accessTokenLifeTime, user);
            await _userService.UpdateRefreshToken(tokenResponse.RefreshToken, user, tokenResponse.Expiration, accessTokenLifeTime);
            return tokenResponse;
        }
    }
}
