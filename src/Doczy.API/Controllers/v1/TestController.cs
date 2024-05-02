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
        public IActionResult Index()
        {
            Url= _videoMeetingService.CreateZoomAsync("c#",30,)
        }
    }
}
