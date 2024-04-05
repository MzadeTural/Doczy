using Doczy.Business.DTOs.Common;
using Doczy.Business.DTOs.DoctorCategoryDtos;
using Doczy.Business.DTOs.FieldOfStudyDtos;
using Doczy.Business.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Doczy.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorCategoriesController : ControllerBase
    {
        private readonly IDoctorCategoryService _doctorCategoryService;

        public DoctorCategoriesController(IDoctorCategoryService doctorCategoryService)
        {
            _doctorCategoryService = doctorCategoryService;
        }

        [HttpPost("")]
         [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<IActionResult> CreateCategoryAsync( CreateDoctorCategoryDto createCategoryDto)
        {
            var response = await _doctorCategoryService.CreateCategoryAsync(createCategoryDto);
            return StatusCode((int)HttpStatusCode.Created, new ResponseDto(response.StatusCode, response.Message));
        }
        [HttpGet("")]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var response = await _doctorCategoryService.GetCategoryAsync();
            return Ok(response);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetCategoriIdsAsync()
        {
            var response = await _doctorCategoryService.GetCategoryIdAsync();
            return Ok(response);
        }
    }
}
