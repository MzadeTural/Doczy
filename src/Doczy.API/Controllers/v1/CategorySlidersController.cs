using Doczy.Business.DTOs.CategorySliderDtos;
using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.HospitalDtos;
using Doczy.Business.DTOs.WorkPlace;
using Doczy.Business.Services.Implementations;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorySlidersController : ControllerBase
    {
        private readonly ICategorySliderService _categorySliderService;

        public CategorySlidersController(ICategorySliderService categorySliderService)
        {
            _categorySliderService = categorySliderService;
        }

        [HttpPost]
        [Route("")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> Create([FromForm] CreateCategorySliderDto createDto)
        {

            var response = await _categorySliderService.CreateAsync(createDto);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));

        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            var slides = await _categorySliderService.GetAsync();
            return Ok(slides);
        }
        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> GetHospitalById(Guid Id)
        {
            var slide = await _categorySliderService.GetByIdAsync(Id);
            return Ok(slide);
        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> DeleteHospital(Guid Id)
        {
            var response = await _categorySliderService.DeleteAsync(Id);
            return StatusCode((int)HttpStatusCode.OK, new ResponseDto(response.StatusCode, response.Message));
        }
    }
}
