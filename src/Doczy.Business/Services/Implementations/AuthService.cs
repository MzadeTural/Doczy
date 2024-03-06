using Doczy.Business.DTOs.AuthDtos;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.MailDtos;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Business.Exceptions.AuthExceptions;
using Doczy.Business.Exceptions.AuthExceptions.Token;
using Doczy.Business.Exceptions.UserExceprions;
using Doczy.Business.Helpers.Extensions;
using Doczy.Business.HelperServices.Interfaces;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        private readonly IOTPService _oTPService;
        private readonly IWebHostEnvironment _environment;
        private readonly IBaseAppUserRepository _baseAppUserRepository;
        private readonly IDoctorRepository _doctorRepository;


        public AuthService(UserManager<BaseAppUser> userManager, IMailService mailService, IUserService userService, ITokenHandler tokenHandler, IHttpContextAccessor httpContextAccessor, SignInManager<BaseAppUser> signInManager, IOTPService oTPService, IBaseAppUserRepository baseAppUserRepository, IDoctorRepository doctorRepository)
        {
            _userManager = userManager;
            _mailService = mailService;
            _userService = userService;
            _tokenHandler = tokenHandler;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _oTPService = oTPService;
            _baseAppUserRepository = baseAppUserRepository;
            _doctorRepository = doctorRepository;
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

        public async Task<ResponseDto> ForgotPasswordAsync(ForgotPasswordRequestDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.EmailOrPhoneNumber) ?? await _userManager.FindByNameAsync(model.EmailOrPhoneNumber);
            if (user is null)
                throw new UserNotFoundException($"User not found by email or phone: {model.EmailOrPhoneNumber}", HttpStatusCode.BadRequest);

            var otp = _oTPService.GenerateOTP();
            user.OTP = otp;
            user.OTPExpiryDate = DateTime.UtcNow.AddMinutes(5);
            await _userManager.UpdateAsync(user);

            if (IsValidEmail(model.EmailOrPhoneNumber))
            {

                string body = await _mailService.GetEmailTemplateAsync(otp, "EmailConfirmationOTP.html");
                await _mailService.SendEmailAsync(new MailRequestDto { ToEmail = user.Email, Subject = "Password Reset OTP", Body = body });

            }
            //else
            //{
            //    _smsService.SendSMS(user.PhoneNumber, $"Your OTP is: {otp}");
            //}

            return new ResponseDto
          (
              StatusCode: HttpStatusCode.OK,
              Message: "OTP sent successfully"
          );

        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto model, int accessTokenLifeTime)
        {
            var loginCheck = _httpContextAccessor?.HttpContext?.User?.Identity;

            if (loginCheck?.IsAuthenticated == true)
                throw new AlreadyAuthenticationException("You are already authenticated", HttpStatusCode.BadGateway);
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
                throw new AuthenticationFailException();
            if (!user.IsVerified)
                throw new UserNotVerifiedException();
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

        public async Task<ResponseDto> ConfirmOTPAsync(ConfirmOTPDto model)
        {
            // var user = await _userManager.FindByEmailAsync(model.EmailOrPhoneNumber) ?? await _userManager.FindByNameAsync(model.EmailOrPhoneNumber);   

            var user = await _baseAppUserRepository.GetUserByEmailOrPhoneNumberAsync(model.EmailOrPhoneNumber);

            if (user == null)
                throw new UserNotFoundException($"User not found by email: {model.EmailOrPhoneNumber}", HttpStatusCode.BadRequest);

            if (user.OTP != model.OTP || user.OTPExpiryDate < DateTime.UtcNow)
                return new ResponseDto(StatusCode: HttpStatusCode.BadRequest, Message: "Invalid or expired OTP.");

            return new ResponseDto
           (
               StatusCode: HttpStatusCode.OK,
               Message: "OTP confirmed successfully"
           );



        }

        public async Task<ResponseDto> ResetPasswordAsync(ResetPasswordDto model)
        {
            var user = await _baseAppUserRepository.GetUserByEmailOrPhoneNumberAsync(model.EmailorPhoneNumber);
            if (user == null)
                throw new UserNotFoundException($"User not found by email or phone: {model.EmailorPhoneNumber}", HttpStatusCode.BadRequest);
            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if (user.OTP != model.OTP || user.OTPExpiryDate < DateTime.UtcNow)
                return new ResponseDto(StatusCode: HttpStatusCode.BadRequest, Message: "Invalid or expired OTP.");
            var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);

            if (resetPasswordResult.Succeeded)
            {
                user.OTP = null;
                user.OTPExpiryDate = null;
                _baseAppUserRepository.Update(user);
            }
            return new ResponseDto
            (
                StatusCode: resetPasswordResult.Succeeded ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                Message: resetPasswordResult.Succeeded ? "Password reset successful" : String.Join(',', resetPasswordResult.Errors.Select(e => e.Description))
            );


        }
    }
}
