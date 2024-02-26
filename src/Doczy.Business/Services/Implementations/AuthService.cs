using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Business.Exceptions.AuthExceptions;
using Doczy.Business.Exceptions.UserExceprions;
using Doczy.Business.Helpers.Extensions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class AuthService : IAuthService
    {

        private readonly UserManager<BaseAppUser> _userManager;

        private readonly IMailService _mailService;
        public AuthService(UserManager<BaseAppUser> userManager, IMailService mailService)
        {
            _userManager = userManager;
            _mailService = mailService;
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


    }
}
