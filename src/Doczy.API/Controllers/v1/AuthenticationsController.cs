using Doczy.Business.DTOs.AuthDtos;
using Doczy.Business.DTOs.Common;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Net;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthenticationsController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login( LoginDto loginUserDto)
        {
            var response = await _authService.LoginAsync(loginUserDto, 15);
            return Ok(response);
        }
        [HttpPost("[Action]")]
        public async Task<IActionResult> LogOut()
        {
            await _authService.LogOutAsync();
            return Ok();

        }
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordRequestDto model)
        {
            var response= await _authService.ForgotPasswordAsync(model);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromForm] ResetPasswordDto model)
        {
            var response = await _authService.ResetPasswordAsync(model);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));

        }
        [HttpPatch("verify-doctor")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> VerifiedDoctor(Guid doctorId )
        {
            var response = await _authService.VerifiedDoctorAsync(doctorId);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
        
    }
}
