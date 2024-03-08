using Doczy.Business.DTOs.UserDtos;
using Doczy.Business.Enums;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IAuthService _authService;

        public UsersController(RoleManager<IdentityRole<Guid>> roleManager, IAuthService authService)
        {
            _roleManager = roleManager;
            _authService = authService;
        }
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailDto confirmEmailDto)
        {
            var response = await _authService.ConfirmEmailAsync(confirmEmailDto);
            return StatusCode((int)response.StatusCode, response.Message);
        }
        [HttpPost("refresh-token-login")]
        public async Task<IActionResult> RefreshTokenLogin(string refreshToken)
        {
            var response = await _authService.RefreshTokenLoginAsync(refreshToken);
            return Ok(response);
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
