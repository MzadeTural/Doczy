using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Business.Enums;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public UsersController(RoleManager<IdentityRole<Guid>> roleManager, IAuthService authService, IUserService userService)
        {
            _roleManager = roleManager;
            _authService = authService;
            _userService = userService;
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailDto confirmEmailDto)
        {
            var response = await _authService.ConfirmEmailAsync(confirmEmailDto);
            return StatusCode((int)response.StatusCode, response.Message);
        }
        [HttpGet("profile-info")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin,Doctor")]
        public async Task<IActionResult> GetAuthUserInfo()
        {
            var response = await _userService.GetAuthUserInfo();
            return Ok(response);
        }
        [HttpPost("refresh-token-login")]
        public async Task<IActionResult> RefreshTokenLogin(string refreshToken)
        {
            var response = await _authService.RefreshTokenLoginAsync(refreshToken);
            return Ok(response);
        }
        [HttpPatch("change-profile-photo")]
        public async Task<IActionResult> CahangeProfilePhoto([FromForm]UpdateProfilePhotoDto model)
        {
            var response = await _userService.ChangeProfilePhoto(model);
            return StatusCode((int) HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpPatch("remove-profile-photo")]
        public async Task<IActionResult> RemoveProfilePhoto( )
        {
            var response = await _userService.RemoveProfilePhotoAsync();
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }

        [HttpPost("createrole")]

        public async Task CreateRole()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = role.ToString() });
                }
            }
        }
    }
}
