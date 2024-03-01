using Doczy.Business.DTOs.AuthDtos;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Login([FromForm] LoginDto loginUserDto)
        {
            var response = await _authService.LoginAsync(loginUserDto, 15);
            return Ok(response);
        }
    }
}
