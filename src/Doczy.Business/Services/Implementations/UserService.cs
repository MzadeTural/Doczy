using AutoMapper;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.MailDtos;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Business.Enums;
using Doczy.Business.Exceptions.UserExceprions;
using Doczy.Business.Services.Interfaces;
using Doczy.Core.Entities.Identities;
using Doczy.DataAccess.Contexts;
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

        public UserService(UserManager<BaseAppUser> userManager, IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator, IWebHostEnvironment environment, IMapper mapper, DoczyContext context, IFileService fileService = null, IMailService mailService = null)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _linkGenerator = linkGenerator;
            _environment = environment;
            _mapper = mapper;
            _context = context;
            _fileService = fileService;
            _mailService = mailService;
        }
        public Task<ResponseDto> CreateAsync(CreateUserDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> CreateDoctorAsync(CreateDoctorDto model)
        {

            string diplomaFile = await _fileService.CreateFileAsync(model.DiplomaImageUrl, _environment.WebRootPath + "/uploads/doctors/diploma/");
            string idCardFile = await _fileService.CreateFileAsync(model.IdCardImageUrl, _environment.WebRootPath + "/uploads/doctors/idcard/");

            var doct = _mapper.Map<DoctorAppUser>(model);
            doct.DiplomaImageUrl = diplomaFile;
            doct.IdCardImageUrl = idCardFile;


            IdentityResult result = await _userManager.CreateAsync(doct, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(doct, Roles.Member.ToString());

                string? url = await GetEmailConfirmationLinkAsync(doct);
                string body = await GetEmailConfirmationTemplate(url);
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
            return url;
        }
        private async Task<string> GetEmailConfirmationTemplate(string url)
        {
            string path = Path.Combine(_environment.WebRootPath, "templates", "EmailConfirmation.html");
            using StreamReader streamReader = new StreamReader(path);
            string result = await streamReader.ReadToEndAsync();
            var body = result.Replace("[Link]", url);
            return body;
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
    }
}
