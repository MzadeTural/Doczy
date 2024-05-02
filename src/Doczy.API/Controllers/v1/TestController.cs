using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private IVideoMeetingService _videoMeetingService;

        public TestController(IVideoMeetingService videoMeetingService)
        {
            _videoMeetingService = videoMeetingService;
        }
        [HttpGet("")]
        public  async Task<IActionResult> Index()
        {
            var url = await _videoMeetingService.CreateZoomAsync("c#", 40, "2024-05-02 00:00:00.0000000", "23:55:00");
            return Ok(url);
        }
    }
}
