using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Doczy.Business.Services.Interfaces;
using Doczy.Business.DTOs.UserDtos;
using Doczy.Business.DTOs.DoctorDtos;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        
        private readonly IUserService _userService;
        private readonly IDoctorService _doctorService;
        public DoctorsController(IUserService userService, IDoctorService doctorService)
        {

            _userService = userService;
            _doctorService = doctorService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] CreateDoctorDto createDoctorDto)
        {
            var response = await _userService.CreateDoctorAsync(createDoctorDto);

            return StatusCode((int)response.StatusCode, response.Message);
        }
        [HttpGet("{userId}/doctor-appointments")]
        public async Task<List<GetDoctorAppointmentsDto>> GetUserCars(Guid userId)
        {
            var appointments = await _doctorService.GetDoctorAppointments(userId);
            return appointments;
        }

        // GET: api/<DoctorsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<DoctorsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<DoctorsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DoctorsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DoctorsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
