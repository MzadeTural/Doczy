using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.MailDtos;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Business.Enums;
using Doczy.Business.Exceptions.UserExceprions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Contexts;
using Doczy.DataAccess.Repositories.Implementations;
using Doczy.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using System.Net;

namespace Doczy.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<BaseAppUser> _userManager;
        private readonly DoczyContext _context;
        private readonly IFileService _fileService;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly IGenderRepository _genderRepository;
        private readonly IBaseAppUserRepository _baseAppUserRepository;

        public UserService(UserManager<BaseAppUser> userManager, IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator, IWebHostEnvironment environment, IMapper mapper, DoczyContext context, IFileService fileService = null, IMailService mailService = null, IGenderRepository genderRepository = null, IBaseAppUserRepository baseAppUserRepository = null)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
            _environment = environment;
            _mapper = mapper;
            _context = context;
            _fileService = fileService;
            _mailService = mailService;
            _genderRepository = genderRepository;
            _baseAppUserRepository = baseAppUserRepository;
        }
        public async Task<ResponseDto> CreateAsync(CreateUserDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> CreateDoctorAsync(CreateDoctorDto model)
        {
            string[] allowedContentTypes = { "image/jpeg", "image/png", "application/pdf" };
            string diplomaFile = await _fileService.CreateFileAsync(model.DiplomaImageUrl, _environment.WebRootPath + "/uploads/users/doctors/diploma/", allowedContentTypes);
            string idCardFile = await _fileService.CreateFileAsync(model.IdCardImageUrl, _environment.WebRootPath + "/uploads/users/doctors/idcard/", allowedContentTypes);
            var doct = _mapper.Map<DoctorAppUser>(model);
            doct.CreatedAt = DateTime.Now;
            doct.DiplomaImageUrl = diplomaFile;
            doct.IdCardImageUrl = idCardFile;
            doct.ProfileImageUrl = "default/profile-circle.svg";
            doct.IsVerified = false;


            IdentityResult result = await _userManager.CreateAsync(doct, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(doct, Roles.Doctor.ToString());
                string? url = await GetEmailConfirmationLinkAsync(doct);
                string body = await _mailService.GetEmailTemplateAsync(url, "EmailConfirmation.html");
                await _mailService.SendEmailAsync(new MailRequestDto { ToEmail = doct.Email, Subject = "Doczy email confirmation for activate account", Body = body });

                var response = new ResponseDto(StatusCode: HttpStatusCode.Created, Message: "Doctor successfully created. To login to your account, please activate your account by clicking on the link sent to your email address.");
                return response;
            }

            throw new UserCreateFailedException(result.Errors);
        }
        private async Task<string?> GetEmailConfirmationLinkAsync(BaseAppUser user)
        {
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            HttpContext? httpContext = _httpContextAccessor.HttpContext;
            string? url = string.Empty;
            if (httpContext is not null)
            {
                //var sessionValue = httpContext.Session.GetString("SessionKey");
                HttpRequest request = httpContext.Request;
                url = _linkGenerator.GetUriByAction(
                   httpContext,
                   action: "ConfirmEmail",
                   controller: "Users",
                   values: new { token, email = user.Email },
                   scheme: request.Scheme,
                   host: request.Host
               );
            }
            string redirectUrl = $"http://localhost:3000/Auth/VerifyEmailMessage?email={System.Web.HttpUtility.UrlEncode(user.Email)}&token={System.Web.HttpUtility.UrlEncode(token)}";
            return redirectUrl;
        }


        public async Task UpdateRefreshToken(string refreshToken, BaseAppUser user, DateTime accessTokenEndDate, int refreshTokenLifeTime)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndDate = accessTokenEndDate.AddMinutes(refreshTokenLifeTime);

                await _userManager.UpdateAsync(user);
                return;
            }

            throw new UserNotFoundException("User cannot be null");
        }

        public async Task<ResponseDto> CreatePatientAsync(CreatePatientDto model)
        {
            var user = _mapper.Map<PatientAppUser>(model);
            user.CreatedAt = DateTime.Now;
            user.ProfileImageUrl = "default/profile-circle.svg";
            user.IsVerified = true;
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Patient.ToString());
                string? url = await GetEmailConfirmationLinkAsync(user);
                string body = await _mailService.GetEmailTemplateAsync(url, "EmailConfirmation.html");
                await _mailService.SendEmailAsync(new MailRequestDto { ToEmail = user.Email, Subject = "Doczy email confirmation for activate account", Body = body });
                var response = new ResponseDto(StatusCode: HttpStatusCode.Created, Message: "Patient successfully created. To login to your account, please activate your account by clicking on the link sent to your email address.");
                return response;
            }

            throw new UserCreateFailedException(result.Errors);
        }

        public async Task<ResponseDto> ChangeProfilePhoto(UpdateProfilePhotoDto model)
        {
            string profilePhoto = await _fileService.CreateFileAsync(model.fileUrl, _environment.WebRootPath + "/uploads/users/profilephotos/");
            var userId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                throw new UserNotFoundException("id",userId.ToString());
            user.ProfileImageUrl = profilePhoto;
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            { 
                return new ResponseDto(
                    StatusCode: HttpStatusCode.NoContent,
                    Message: "Profile photo successfully updated"
                );
            }
            return new ResponseDto(
            StatusCode: HttpStatusCode.InternalServerError,
            Message: $"Failed to update profile photo,\nErrors:{result.Errors}"
            );

        }

        public async Task<GetUserDto> GetAuthUserInfo()
        {
            var userId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            var user =await _baseAppUserRepository.GetSingleAysnc(ba => ba.Id == userId && ba.IsVerified, ba => ba.Gender);
            if (user is null)
                throw new UserNotFoundException("User Not Found");
            var userInfo = _mapper.Map<GetUserDto>(user);

            return userInfo;
        }

        public async Task<ResponseDto> RemoveProfilePhotoAsync()
        {
            var userId = (await _userManager.GetUserAsync(_httpContextAccessor?.HttpContext?.User)).Id;
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user is null)
                throw new UserNotFoundException("id",userId.ToString());
              _fileService.DeteleFile( _environment.WebRootPath + $"/uploads/users/profilephotos/{user.ProfileImageUrl}");
            user.ProfileImageUrl = "default/profile-circle.svg";
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
            return new ResponseDto(
                    StatusCode: HttpStatusCode.NoContent,
                    Message: "Profile photo successfully removed"
                );
            }
            return new ResponseDto(
           StatusCode: HttpStatusCode.InternalServerError,
           Message: $"Failed to remove profile photo,\nErrors:{result.Errors}"
           );
        }
    }
}
